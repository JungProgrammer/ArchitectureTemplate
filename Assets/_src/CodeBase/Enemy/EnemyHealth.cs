using System;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] 
        private EnemyAnimator _enemyAnimator;

        
        [SerializeField] 
        private float _current;

        
        [SerializeField] 
        private float _max;

        public event Action HealthChanged;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            
            _enemyAnimator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}