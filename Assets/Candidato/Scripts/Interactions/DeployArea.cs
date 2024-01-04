using Candidato.Scripts.Items;
using UnityEngine;

namespace Candidato.Scripts.Interactions
{
    
    public class DeployArea : MonoBehaviour
    {
        /// <summary>
        ///   <para>Clears Inventory of any GameObject that collides by trigger with it. Compares Tag with "Player" and "NPC" for safety</para>
        /// </summary>
        /// <param name="other">The GameObject to check.</param>
        private void OnTriggerEnter(Collider other)
        { 
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Npc")) return;
            other.gameObject.GetComponent<Inventory>().ClearInventory(other.gameObject);
        }
    }
}

