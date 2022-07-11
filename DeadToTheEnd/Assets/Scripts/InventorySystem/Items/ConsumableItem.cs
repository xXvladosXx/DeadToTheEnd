using System.Text;
using Data.Stats;
using Entities.Core;
using UnityEngine;

namespace InventorySystem
{
    public abstract class ConsumableItem : InventoryItem
    {
        [SerializeField] protected float Value;
        
        public abstract void UseItem(AliveEntity aliveEntity);
        public override string GetInfoDisplayText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Rarity.Name).AppendLine();
            stringBuilder.Append(ItemType).AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.Append(Description + $"<i>{Value}</i>").AppendLine();
            stringBuilder.Append("Max stack: ").Append(MaxItemsInInventorySlot).AppendLine();
            stringBuilder.Append("Sell price: ").Append(SellPrice).AppendLine();

            return stringBuilder.ToString();
            
        }
    }
}