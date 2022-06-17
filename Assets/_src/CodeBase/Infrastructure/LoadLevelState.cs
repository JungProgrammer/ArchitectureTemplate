using _src.CodeBase.Cameralogic;
using _src.CodeBase.Hero;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        
        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        
        public void Exit()
        {
            _curtain.Hide();
        }


        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPoint));
            _gameFactory.CreateHud();
            
            CameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
        }


        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}