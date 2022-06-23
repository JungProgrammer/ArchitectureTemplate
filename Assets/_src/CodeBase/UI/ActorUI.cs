using System;
using _src.CodeBase.Hero;
using UnityEngine;

namespace _src.CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField]
        private HPBar _hpBar;


        private HeroHealth _heroHealth;


        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;

            _heroHealth.HealthChanged += UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}