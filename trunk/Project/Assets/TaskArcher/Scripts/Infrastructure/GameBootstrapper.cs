using TaskArcher.Infrastructure.States;
using TaskArcher.UI;
using UnityEngine;

namespace TaskArcher.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public Game game;
        [SerializeField] private BaseUIRoot baseUIRoot;
        
        private void Awake()
        {
            game = new Game(this, baseUIRoot);
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
