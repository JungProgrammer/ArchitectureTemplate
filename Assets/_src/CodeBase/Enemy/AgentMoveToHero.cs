using System;
using _src.CodeBase.Infrastructure.Factory;
using _src.CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _src.CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        private const float MinimalDistance = 1;
        
        
        [SerializeField] 
        private NavMeshAgent _agent;


        private Transform _heroTransform;
        private IGameFactory _gameFactory;


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
            if (Initialized() && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool Initialized()
        {
            return _heroTransform != null;
        }

        private void OnHeroCreated()
        {
            InitializeHeroTransform();
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private bool HeroNotReached()
            => Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}