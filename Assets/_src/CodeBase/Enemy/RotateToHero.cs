using _src.CodeBase.Infrastructure.Factory;
using _src.CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _src.CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField]
        private float _speed;
        
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;


        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }
        
        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        private bool HeroExists()
            => _gameFactory.HeroGameObject != null;

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 postionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(postionDiff.x, transform.position.y, postionDiff.z);
        }
        
        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
            => Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook)
            => Quaternion.LookRotation(positionToLook);

        private float SpeedFactor()
            => _speed * Time.deltaTime;

        private bool Initialized()
            => _heroTransform != null;

        private void OnHeroCreated()
        {
            InitializeHeroTransform();
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }
    }
}