using UnityEngine;

namespace InventorySystem
{
    public class ItemEquipper : MonoBehaviour
    {   
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public Inventory Equipment { get; private set; }
    }
}