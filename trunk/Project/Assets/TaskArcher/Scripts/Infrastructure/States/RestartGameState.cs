using TaskArcher.Infrastructure.AssetManagement;
using TaskArcher.Infrastructure.Services.CustomEventBus;

namespace TaskArcher.Infrastructure.States
{
    public class RestartGameState: IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IEventBus _eventBus;
        
        public RestartGameState(GameStateMachine stateMachine, IEventBus eventBus)
        {
            _stateMachine = stateMachine;
            _eventBus = eventBus;
        }
        
        public void Enter()
        {
            _stateMachine.Enter<InGameState, string>(AssetPath.TaskArcherScene);
        }

        public void Exit()
        {

        }
    }
}