using _src.CodeBase.Cameralogic;
using _src.CodeBase.Infrastructure;
using _src.CodeBase.Services.Input;
using UnityEngine;

namespace _src.CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;


        [SerializeField] 
        private float _movementSpeed;


        private IInputService _inputService;
        private Camera _camera;


        private void Awake()
        {
            _inputService = Game.InputService;
        }


        private void Start()
        {
            _camera = Camera.main;
            
            CameraFollow();
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
        
        
        private void CameraFollow()
        {
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
        }
    }
}
