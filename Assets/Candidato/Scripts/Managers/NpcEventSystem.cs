using System.Collections;
using Candidato.Scripts.Input_Actions;
using Candidato.Scripts.Items;
using UnityEngine;

namespace Candidato.Scripts.Managers
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(AnimationState))]
    public class NpcEventSystem : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private AnimationState _animationState;
        
        private Coroutine _npcBehaviorCorrutine;
        private GameObject _deployArea;
        private Movement _movement;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _deployArea = GameObject.FindWithTag("DeployArea");
            _movement = GetComponent<Movement>();
            _animationState = GetComponent<AnimationState>();
            _npcBehaviorCorrutine = StartCoroutine(NpcBehavior());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        
        /// <summary>
        ///   <para>Corrutine that handles NPC behavior. Checks Npc inventory. If the inventory is full targets the Deploy area to empty it. If the Npc has space in the inventory fins a random Item on the Scene and walks to it.</para>
        /// </summary>
        private IEnumerator NpcBehavior()
        {
            while (true)
            {
                if (Time.timeScale == 0) yield return null;
                if (_inventory.IsInventoryFull())
                {
                    _movement.SetTarget(_deployArea);
                    yield return new WaitForSeconds(1f);
                    if (!_inventory.IsInventoryFull())
                    {
                        _animationState.SetState(GameplayState.Idle) ;
                    }
                    _movement.SetTarget(null);
                }
                else
                {
                    if (!_movement.GetTarget())
                    {
                        _animationState.SetState(GameplayState.Idle) ;
                        yield return new WaitForSeconds(1f);
                        var items = GameObject.FindGameObjectsWithTag("Item");
                        if(items.Length>0){                    
                            _movement.SetTarget(items[Random.Range(0,items.Length)]);
                        }
                        yield return null;
                    }
                    else
                    {
                        _animationState.SetState(GameplayState.Moving) ;
                    }
                }
                yield return null;
            }
        }
    }
}
