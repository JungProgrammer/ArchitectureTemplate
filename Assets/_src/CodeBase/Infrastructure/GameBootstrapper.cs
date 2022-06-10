using System;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;
        
        
        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
