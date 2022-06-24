using System;
using System.Linq;
using _src.CodeBase.Hero;
using _src.CodeBase.Infrastructure.Factory;
using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] 
        private EnemyAnimator _animator;


        [SerializeField] 
        private float _attackCooldown = 3f;


        [SerializeField]
        private float _cleavage = 0.5f;


        [SerializeField]
        private float _effectiveDistance = 0.5f;

        
        [SerializeField]
        private float _damage = 10;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;


        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 1);

                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
            => new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z) + transform.forward * _effectiveDistance;

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack()
            => _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp()
            => _currentAttackCooldown <= 0;

        private void OnHeroCreated()
            => _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}