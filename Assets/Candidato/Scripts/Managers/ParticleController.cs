using System;
using UnityEngine;

namespace Candidato.Scripts.Managers
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleSystem;
        private AnimationState _animationState;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _animationState = gameObject.GetComponentInParent<AnimationState>();
        }
        
        /// <summary>
        ///   <para>Checks the AnimationState to display or hide the particles. Particles are active if the GameObject is Moving.</para>
        /// </summary>
     private void Update()
        {
            if (_animationState.GetState() == GameplayState.Idle)
            {
                _particleSystem.Pause();
                _particleSystem.Clear();
            }
            else
            {
                _particleSystem.Play();
            }
                
        }
    }
}
