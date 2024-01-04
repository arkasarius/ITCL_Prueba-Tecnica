using UnityEngine;

namespace Candidato.Scripts.Items
{
 [CreateAssetMenu(fileName = "New Item", menuName = "Items")]
 public class Item : ScriptableObject
 {
  [SerializeField] private string itemName;
  [SerializeField] private string itemDescription;
  [SerializeField] private int itemValue;
  [SerializeField] private GameObject displayMesh;

  public string GetName() => itemName;
  public string GetDescription() => itemDescription;
  public int GetValue() => itemValue;
  public GameObject GetDisplayMesh => displayMesh;
 }
}

