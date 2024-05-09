using System.Collections;
using TaskArcher.Infrastructure.AssetManagement;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI;
using TaskArcher.Infrastructure.Services.StaticData;
using TaskArcher.Infrastructure.States;
using TaskArcher.Units;
using TaskArcher.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace TaskArcher.UI
{
    public class InGameScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TabGroup annoTabGroup;

        private BaseUIRoot _baseUIRoot;
        private IEventBus _eventBus;

        private Coroutine _restartGameCoroutine;

        [SerializeField] private Player _player;
        private StaticData _staticData;
        
        private void Start()
        {
            restartButton.onClick.AddListener(RestartScene);
            annoTabGroup.onSelectedTabChanged.AddListener(delegate {SwitchToggles(annoTabGroup); });
        }

        private void OnEnable()
        {
            InitInGameScreen();
        }

        private void InitInGameScreen()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
            _baseUIRoot = FindObjectOfType<BaseUIRoot>();
            _staticData = StaticData.Instance;
        }

        private void FindPlayer()
        {
            if (_player == null) 
            {
                _player = FindObjectOfType<Player>();
            } 
        }

        private void RestartScene()
        {
            if (_restartGameCoroutine != null)
            {
                StopCoroutine(_restartGameCoroutine);
            }

            _restartGameCoroutine = StartCoroutine(RestartGameCoroutine());
        }

        private IEnumerator RestartGameCoroutine()
        {
            _eventBus.Invoke(new FadeInSignal(1f));
            
            yield return new WaitForSeconds(1f);
            
            _baseUIRoot.GameBootstrapper.Game.StateMachine.Enter<InGameState, string>(AssetPath.TaskArcherScene);
            annoTabGroup.SelectDefault();
        }

        private void SwitchToggles(TabGroup tabGroup)
        {
            FindPlayer();

            if (_player != null) {
                _player.Weapon.GetComponent<RangeWeapon>().CurrentBulletDefinition = 
                    tabGroup.TabButtons.IndexOf(tabGroup.SelectedTab) switch {
                        0 => _staticData.GameStaticData.allAmmoDefinitions[0],
                        1 => _staticData.GameStaticData.allAmmoDefinitions[1],
                        _ => _player.Weapon.GetComponent<RangeWeapon>().CurrentBulletDefinition
                    };
            }
        }
    }
}
