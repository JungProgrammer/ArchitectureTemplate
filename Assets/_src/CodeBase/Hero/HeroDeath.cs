using System;
using UnityEngine;

namespace _src.CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] 
        private HeroHealth _heroHealth;


        [SerializeField] 
        private HeroAttack _heroAttack;


        [SerializeField] 
        private HeroMove _heroMove;


        [SerializeField] 
        private HeroAnimator _heroAnimator;


        [SerializeField] 
        private GameObject _deathFx;

        
        private bool _isDead;


        private void Start() => 
            _heroHealth.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            Debug.Log("changed");
            if (!_isDead && _heroHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            _heroAnimator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}