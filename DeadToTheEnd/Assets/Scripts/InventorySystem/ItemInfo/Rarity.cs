using UnityEngine;

namespace InventorySystem.ItemInfo
{
    [CreateAssetMenu(menuName = "InventorySystem/Rarity")]
    public class Rarity : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}