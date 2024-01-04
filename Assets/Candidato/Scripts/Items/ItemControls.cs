using UnityEngine;

namespace Candidato.Scripts.Items
{
    public class ItemControls : MonoBehaviour
    {
        [SerializeField] private Item item;
        
        /// <summary>
        ///   <para>Checks if a Collider with tag "Player" or "NPC" collides with the item. If the collider inventory is not full adds itself to the inventory and destroys the GameObject from the scene.</para>
        /// </summary>
        /// <param name="other">Collider to check tag.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Npc")) return;
            var currentInventory = other.gameObject.GetComponent<Inventory>();
            if (currentInventory.IsInventoryFull()) return;
            if (currentInventory.AddItem(item))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Fallo al añadir objeto en el inventario");
            }
        }
    }
}

