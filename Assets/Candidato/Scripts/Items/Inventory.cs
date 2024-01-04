using System;
using System.Collections.Generic;
using System.Linq;
using Candidato.Scripts.Utils;
using UnityEngine;

namespace Candidato.Scripts.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private int inventoryLimit=3;
        public event Action<List<Item>> OnChange;
        [SerializeField] private GameObject _feedback;
        
        /// <summary>
        ///   <para>Add item to the Items list if we have less items than inventoryLimit.</para>
        /// </summary>
        /// <param name="newItem">Item Added to the pool if we have less than inventoryLimit items.</param>
        public bool AddItem(Item newItem)
        {
            if (_items.Count >= inventoryLimit) return false;
            _items.Add(newItem);
            OnChange?.Invoke(_items);
            return true;
        }
        
        /// <summary>
        ///   <para>Clears the inventory of this GameObject. If we are the Player spawns a _feedback GameObject that shows the points of our items.</para>
        /// </summary>
        /// <param name="prop">GameObject to check the tag. If prop tag is "Player" instantiates a _feedback GameObject.</param>
        public void ClearInventory(GameObject prop)
        {
            if (_items.Count <= 0) return;
            if (prop.CompareTag("Player"))
            {
                var points = _items.Sum(item => item.GetValue());
                Events.OnScoreIncrease(points);
                if (!_feedback) return;
                //localizar rotación del pivote auxiliar de camara para ofrecer feedback relativo a su rotación.
                var camaraRotation = GameObject.FindWithTag("CameraHelper").transform.rotation.eulerAngles.y;
                var feed = Instantiate(_feedback, 
                    prop.gameObject.transform.position+new Vector3(0,1.5f,0),
                    Quaternion.Euler(45,camaraRotation,0));
                feed.GetComponent<PointsFeedback>().SetPoints(points);
            }
            _items.Clear();
            OnChange?.Invoke(_items);
        }

        /// <summary>
        ///   <para>Returns True if we reach the inventory limit. Returns False if we can get more items</para>
        /// </summary>
        public bool IsInventoryFull()
        {
            return _items.Count >= inventoryLimit;
        }
        
        
    }
}



