using TaskArcher.Infrastructure.States;
using TaskArcher.UI;
using UnityEngine;

namespace TaskArcher.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        [SerializeField] private BaseUIRoot baseUIRoot;

        /*public BaseUIRoot BaseUIRoot => baseUIRoot;*/

        private void Awake()
        {
            _game = new Game(this, baseUIRoot);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
