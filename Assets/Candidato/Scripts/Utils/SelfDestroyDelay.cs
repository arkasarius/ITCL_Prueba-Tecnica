using UnityEngine;

namespace Candidato.Scripts.Utils
{
    public class SelfDestroyDelay : MonoBehaviour
    {
        [SerializeField] private float delay;
        
        private void Awake()
        {
            Destroy(gameObject,delay);
        }
    }
}
