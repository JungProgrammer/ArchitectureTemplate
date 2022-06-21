using System;
using System.Collections;
using UnityEngine;

namespace _src.CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] 
        private TriggerObserver _triggerObserver;


        [SerializeField] 
        private Follow _follow;


        [SerializeField] 
        private float _cooldown;


        private Coroutine _aggroCoroutine;
        private bool _isHasAgroTarget;


        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;

            _follow.enabled = false;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (!_isHasAgroTarget)
                return;

            _isHasAgroTarget = false;
            
            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldownRoutine());
        }

        private IEnumerator SwitchFollowOffAfterCooldownRoutine()
        {
            yield return new WaitForSeconds(_cooldown);
            
            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_isHasAgroTarget)
                return;

            _isHasAgroTarget = true;
            
            StopAgroCoroutine();

            SwitchFollowOn();
        }

        private void StopAgroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOff()
        {
            _follow.enabled = false;
        }
        
        private void SwitchFollowOn()
        {
            _follow.enabled = true;
        }
    }
}