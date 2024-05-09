using System.Collections;
using TaskArcher.Infrastructure.AssetManagement;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI;
using TaskArcher.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace TaskArcher.UI
{
    public class InGameScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

        private BaseUIRoot _baseUIRoot;
        private IEventBus _eventBus;

        private Coroutine _restartGameCoroutine;
        
        private void Start()
        {
            restartButton.onClick.AddListener(RestartScene);
        }

        private void OnEnable()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
            _baseUIRoot = FindObjectOfType<BaseUIRoot>();
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
            
            _baseUIRoot.gameBootstrapper.game.StateMachine.Enter<RestartGameState>();
        }

    }
}
