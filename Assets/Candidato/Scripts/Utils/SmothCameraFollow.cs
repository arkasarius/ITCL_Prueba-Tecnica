using UnityEngine;

 namespace Candidato.Scripts.Utils
 {
     public class SmoothCameraFollow : MonoBehaviour
     {
         #region Variables
    
         private Vector3 _offset;
         [SerializeField] private Transform target;
         [SerializeField] private float smoothTime;
         private Vector3 _currentVelocity = Vector3.zero;
        
         #endregion
    
         #region Unity callbacks
        
         /// <summary>
         ///   <para>Sets the offset from the player and the Camera Axis GameObject</para>
         /// </summary>
         private void Awake() => _offset = transform.position - target.position;
         
         /// <summary>
         ///   <para>Updates the offset of the Camera Axis GameObject over smoothTime variable.</para>
         /// </summary>
         /// <param name="smoothTime">Time before smothing the offset of the camera to the player.</param>
         private void LateUpdate()
         {
             var targetPosition = target.position + _offset;
             transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
         }
        
         #endregion
     }
 }
