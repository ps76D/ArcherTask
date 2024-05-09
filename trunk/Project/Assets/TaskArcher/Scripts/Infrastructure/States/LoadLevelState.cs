using TaskArcher.Infrastructure.AssetManagement;
using TaskArcher.Infrastructure.Factory;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.UI;

namespace TaskArcher.Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly BaseUIRoot _baseUIRoot;
        private readonly IEventBus _eventBus;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, BaseUIRoot baseUIRoot, IEventBus eventBus)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _baseUIRoot = baseUIRoot;
            _eventBus = eventBus;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _gameFactory.CreateBaseUIRoot(_baseUIRoot);

            _stateMachine.Enter<InGameState, string>(AssetPath.TaskArcherScene);
            /*EnterInGameState();*/
        }
    }
}