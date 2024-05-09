using TaskArcher.Infrastructure.States;
using TaskArcher.UI;
using UnityEngine;

namespace TaskArcher.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public Game Game;
        [SerializeField] private BaseUIRoot baseUIRoot;
        
        private void Awake()
        {
            Game = new Game(this, baseUIRoot);
            Game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
