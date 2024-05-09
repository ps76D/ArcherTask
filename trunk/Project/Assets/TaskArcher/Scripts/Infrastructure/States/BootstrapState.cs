using TaskArcher.Infrastructure.AssetManagement;
using TaskArcher.Infrastructure.Factory;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using UnityEngine;

namespace TaskArcher.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(AssetPath.InitScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>(AssetPath.UIInit);

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
            
            _services.RegisterSingle<IEventBus>(new EventBus());
            
            Debug.Log("All Services Registered");
        }
    }
}