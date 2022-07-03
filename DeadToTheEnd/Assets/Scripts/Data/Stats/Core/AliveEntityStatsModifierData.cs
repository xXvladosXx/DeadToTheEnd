using System;
using RotaryHeart.Lib.SerializableDictionary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.Stats
{
    [CreateAssetMenu(menuName = "Stats/AliveEntityStatModifier")]
    public class AliveEntityStatsModifierData : ScriptableObject
    {
        [field: SerializeField] public StatModifierSerializableDictionary StatModifier { get; private set; }
    }

    [Serializable]
    public class StatModifierSerializableDictionary : SerializableDictionaryBase<Stat, StatCharacteristicModifier> { }

    [Serializable]
    public class StatCharacteristicModifier
    {
        public float Value;
        public Characteristic Characteristic;
    }
}