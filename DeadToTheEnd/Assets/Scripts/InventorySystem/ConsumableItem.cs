using System.Text;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (menuName = "InventorySystem/ConsumableItem")]
    public class ConsumableItem : Item
    {
        public override string GetInfoDisplayText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).AppendLine();
            stringBuilder.Append(Description).AppendLine();
            stringBuilder.Append("Max stack: ").Append(MaxItemsInInventorySlot).AppendLine();
            stringBuilder.Append("Sell price: ").Append(SellPrice).AppendLine();

            return stringBuilder.ToString();
        }
    }
}