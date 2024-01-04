using UnityEngine;

namespace Candidato.Scripts.Managers
{
    public class AnimationState : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameplayState _gameplayState;
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator> ();
        }

        /// <summary>
        ///   <para>Sets the animation state of this gameObject and the animator.</para>
        /// </summary>
        /// <param name="s">New state. GameplayState can be Idle or Moving</param>
        public void SetState(GameplayState s)
        {
            _gameplayState = s;
            _animator.SetInteger("CharacterState", _gameplayState == GameplayState.Idle ? 0 : 1);
        }
        public GameplayState GetState()
        {
            return _gameplayState;
        }
    }
}
