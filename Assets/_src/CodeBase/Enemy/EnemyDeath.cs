using System;
using System.Collections;
using UnityEngine;

namespace _src.CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] 
        private EnemyHealth _enemyHealth;


        [SerializeField] 
        private EnemyAnimator _enemyAnimator;


        [SerializeField] 
        private GameObject _deathFx;


        public event Action Happened;


        private void Start()
        {
            _enemyHealth.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_enemyHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _enemyHealth.HealthChanged -= OnHealthChanged;
            
            _enemyAnimator.PlayDeath();

            SpawnDeathFx();
            StartCoroutine(DestroyTimerRoutine());
            
            Happened?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimerRoutine()
        {
            yield return new WaitForSeconds(3);
            
            Destroy(gameObject);
        }
    }
}