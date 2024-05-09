using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.States;
using TaskArcher.UI;

namespace TaskArcher.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, BaseUIRoot baseUIRoot)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), baseUIRoot, AllServices.Container);
        }
    }
}