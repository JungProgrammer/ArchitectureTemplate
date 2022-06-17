using _src.CodeBase.Infrastructure.States;
using _src.CodeBase.Logic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain Curtain;
        
        private Game _game;
        
        
        private void Awake()
        {
            _game = new Game(this, Curtain);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
