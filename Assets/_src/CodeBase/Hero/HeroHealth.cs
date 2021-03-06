using System;
using _src.CodeBase.Data;
using _src.CodeBase.Infrastructure.Services.PersistentProgress;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField]
        private HeroAnimator _heroAnimator;
        
        
        private State _state;

        public event Action HealthChanged;
        
        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP != value)
                {
                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHP; 
            set => _state.MaxHP = value;
        }


        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
            _heroAnimator.PlayHit();
        }
    }
}
