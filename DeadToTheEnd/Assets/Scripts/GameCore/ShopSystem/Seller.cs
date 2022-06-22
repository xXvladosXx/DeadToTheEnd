using InventorySystem;
using UnityEngine;

namespace GameCore.ShopSystem
{
    public class Seller : MonoBehaviour
    {
        [field: SerializeField] public ItemContainer ItemContainer { get; private set; }

    }
}