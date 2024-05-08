using TaskArcher.UI;

namespace TaskArcher.Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly BaseUIRoot _baseUIRoot;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, BaseUIRoot baseUIRoot)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _baseUIRoot = baseUIRoot;
        }

        public void Enter(string sceneName) => _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {

        }

        private void OnLoaded()
        {
        }
    }
}