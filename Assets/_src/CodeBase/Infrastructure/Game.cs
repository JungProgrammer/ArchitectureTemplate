using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Infrastructure.States;
using _src.CodeBase.Logic;

namespace _src.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;


        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}