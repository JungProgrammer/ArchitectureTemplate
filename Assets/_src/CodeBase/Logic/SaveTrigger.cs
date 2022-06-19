using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _src.CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }
        
        private void OnDrawGizmos()
        {
            if (Collider == null)
                return;
            
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}