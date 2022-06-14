using _src.CodeBase.Services.Input;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        
        public GameStateMachine StateMachine;


        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}