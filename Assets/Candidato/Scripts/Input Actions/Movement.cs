using UnityEngine;

namespace Candidato.Scripts.Input_Actions
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private GameObject _target;
        private Vector3 _targetLocation;
        

        private void Update()
        {
            if (!_target) return;
            var step =  speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, _targetLocation, step);
            transform.LookAt(_targetLocation,Vector3.up);
        }
        /// <summary>
        ///   <para>Sets a target location for NPC logic</para>
        /// </summary>
        /// <param name="tar">The GameObject reference to get the location. Forced to 0 on Y for this game as we move on a plane.</param>
        public void SetTarget(GameObject tar)
        {
            _target = tar;
            if(!_target) return;
            var position = _target.transform.position;
            position=new Vector3(position.x,0,position.z);
            _targetLocation = position;
        }
        /// <summary>
        ///   <para>Sets a target location for Player logic</para>
        /// </summary>
        /// <param name="tar">The Vector3 of Raycast from camera to world for player movement.</param>
        public void SetTargetPlayer(Vector3 tar)
        {
            if (!gameObject.CompareTag("Player")) return;
            var position=new Vector3(tar.x,0,tar.z);
            _target = gameObject;
            _targetLocation = position;
        }
        /// <summary>
        ///   <para>Returns current GameObject set as target. Returns null if no target is referenced.</para>
        /// </summary>
        public GameObject GetTarget()
        {
            return _target;
        }
        
    }
}
