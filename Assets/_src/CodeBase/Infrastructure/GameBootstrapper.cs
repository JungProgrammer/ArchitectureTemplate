using _src.CodeBase.Infrastructure.States;
using _src.CodeBase.Logic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField]
        private LoadingCurtain CurtainPrefab;
        
        private Game _game;
        
        
        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
