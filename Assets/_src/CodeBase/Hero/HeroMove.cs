using System;
using _src.CodeBase.Cameralogic;
using _src.CodeBase.Data;
using _src.CodeBase.Infrastructure;
using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Infrastructure.Services.PersistentProgress;
using _src.CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _src.CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField]
        private CharacterController _characterController;


        [SerializeField] 
        private float _movementSpeed;


        private IInputService _inputService;
        private Camera _camera;


        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }


        private void Start()
        {
            _camera = Camera.main;
        }


        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            
            Debug.Log(_inputService);
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
        
        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(SceneManager.GetActiveScene().name, transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (SceneManager.GetActiveScene().name != progress.WorldData.PositionOnLevel.Level)
                return;
            
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition == null)
                return;
            
            
            Warp(to: savedPosition);
        }
        
        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }
    }
}
