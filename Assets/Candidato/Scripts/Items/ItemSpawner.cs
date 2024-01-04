using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Candidato.Scripts.Items
{
   public class ItemSpawner : MonoBehaviour
   {
      [SerializeField] private List<GameObject> itemSpawnPool;
      [SerializeField] private float timer, variance;

      private void Awake()
      {
         StartCoroutine(SpawnItem());
      }
      
      /// <summary>
      ///   <para>Instantiates a random Item from the itemSpawnPool list of Items each time the timer sets to 0.</para>
      /// </summary>
      private IEnumerator SpawnItem()
      {
         while (true)
         {
            yield return new WaitForSeconds(timer + Random.Range(-variance, variance));
            Instantiate(itemSpawnPool[Random.Range(0, itemSpawnPool.Count)], transform.position, Quaternion.identity);
         }
      }
   }
}
