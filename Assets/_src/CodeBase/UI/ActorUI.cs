using System;
using _src.CodeBase.Hero;
using _src.CodeBase.Logic;
using UnityEngine;

namespace _src.CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField]
        private HPBar _hpBar;


        private IHealth _health;


        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            
            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}