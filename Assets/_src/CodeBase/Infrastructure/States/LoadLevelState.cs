using _src.CodeBase.Cameralogic;
using _src.CodeBase.Infrastructure.Factory;
using _src.CodeBase.Infrastructure.Services.PersistentProgress;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        
        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        
        public void Exit()
        {
            _curtain.Hide();
        }


        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPoint));
            _gameFactory.CreateHud();
            CameraFollow(hero);
        }


        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}