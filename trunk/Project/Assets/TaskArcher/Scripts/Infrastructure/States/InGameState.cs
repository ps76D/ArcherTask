using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI;

namespace TaskArcher.Infrastructure.States
{
    public class InGameState: IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IEventBus _eventBus;
        
        public InGameState(GameStateMachine stateMachine, SceneLoader sceneLoader, IEventBus eventBus)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _eventBus = eventBus;
        }
        
        public void Enter(string sceneName)
        {
            _sceneLoader.ReLoad(sceneName, OnLoaded);
        }

        public void Exit()
        {

        }

        private void OnLoaded()
        {
            _eventBus.Invoke(new FadeOutSignal(2f));
        }
    }
}