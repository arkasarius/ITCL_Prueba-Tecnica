using System.Collections.Generic;
using UnityEngine;


namespace Candidato.Scripts.Items
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject itemHolder;
        [SerializeField] private Inventory inventory;
    
        /// <summary>
        ///   <para>Checks if we have a child gameObject with the InventoryDisplay tag. Disables script if its not present.</para>
        /// </summary>
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("InventoryDisplay"))
                {
                    itemHolder = child.gameObject;
                }
            }
            if (!itemHolder)
            {
                enabled = false;
                return;
            }

            inventory = GetComponent<Inventory>();
            
        }
        
        /// <summary>
        ///   <para>Subscribe to the inventory Onchange event.</para>
        /// </summary>
        private void OnEnable()
        {
            if (inventory)
                inventory.OnChange += DisplayItems;
        }
        
        /// <summary>
        ///   <para>Unsubscribes from the inventory OnChange event.</para>
        /// </summary>
        private void OnDisable()
        {
            if (inventory)
                inventory.OnChange -= DisplayItems;
        }

        /// <summary>
        ///   <para>Clears the display GameObjects childs then instantiates the corresponding ones with correct position on the ItemHolder parent.</para>
        /// </summary>
        /// <param name="items">List of current items on the inventory.</param>
        private void DisplayItems(List<Item> items)
        {
            foreach (Transform child in itemHolder.transform) {
                Destroy(child.gameObject);
            }

            switch (items.Count)
            {
                case 0:
                    break;
                case 1:
                    SpawnItems(items[0],new Vector3(0,.6f,1));
                    break;
                case 2:
                    SpawnItems(items[0],new Vector3(0,.6f,1));
                    SpawnItems(items[1],new Vector3(0,.6f,1.5f));
                    break;
                case 3:
                    SpawnItems(items[0],new Vector3(0,.6f,1));
                    SpawnItems(items[1],new Vector3(0,.6f,1.5f));
                    SpawnItems(items[2],new Vector3(0,.6f,2));
                    break;
                
            }
            
        }
        
        /// <summary>
        ///   <para>Instantiates and item mesh at target offset from itemHolder parent. </para>
        /// </summary>
        /// <param name="item">Checks the Item DisplayMesh GameObject to instantiate a child mesh.</param>
        /// <param name="pos">Child offset from parent root.</param>
        private void SpawnItems(Item item, Vector3 pos)
        {
            var spawnPoint = itemHolder.transform;
            var instance= Instantiate(item.GetDisplayMesh,spawnPoint,false);
            instance.transform.localPosition = pos;
            //rotar objeto instanciado para dar más dinamismo a la pose del objeto en la mochila.
            instance.transform.rotation = Quaternion.Euler(-90, 0, -90);
        }
    
    }
}
