using System;
using SaveSystem;
using UnityEngine;

namespace Data.Shop
{
    [Serializable]
    public class Gold : ISavable
    {
        [field: SerializeField] public int GoldAmount { get; private set; }

        public event Action OnGoldChanged;

        public void AddGold(int amount)
        {
            GoldAmount += amount;
        }

        public void RemoveGold(int amount)
        {
            GoldAmount -= amount;
        }

        public object CaptureState()
        {
            return GoldAmount;
        }

        public void RestoreState(object state)
        {
            GoldAmount = (int) state;
        }
    }
}