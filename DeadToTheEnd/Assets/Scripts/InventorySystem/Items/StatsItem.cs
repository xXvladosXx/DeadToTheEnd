using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Stats;
using Data.Stats.Core;
using StatsSystem.Bonuses;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (menuName = "InventorySystem/StatsItem")]
    public class StatsItem : UpgradableItem, IModifier
    {
        [SerializeField] protected float _damageBonus = 0;
        [SerializeField] protected float _healthBonus = 0;
        [SerializeField] protected float _criticalDamage = 0;
        [SerializeField] protected float _criticalChance;
        [SerializeField] protected float _attackSpeedBonus;
        [SerializeField] protected float _movementSpeedBonus;
        [SerializeField] protected float _healthRegenerationBonus;
        [SerializeField] protected float _manaBonus;
        [SerializeField] protected float _manaRegenerationBonus;
        [SerializeField] protected float _evasionBonus;
        [SerializeField] protected float _accuracyBonus;

        public IEnumerable<IBonus> AddBonus(Stat[] stat)
        {
            IBonus BonusTo(Stat stats)
            {
                return stats switch
                {
                    Stat.Damage => new DamageBonus(_damageBonus),
                    Stat.Health => new HealthBonus(_healthBonus),
                    Stat.CriticalChance => new CriticalChanceBonus(_criticalChance),
                    Stat.CriticalDamage => new CriticalDamageBonus(_criticalDamage),
                    Stat.AttackSpeed => new AttackSpeedBonus(_attackSpeedBonus),
                    Stat.MovementSpeed => new MovementSpeedBonus(_movementSpeedBonus),
                    Stat.ManaRegeneration => new ManaRegenerationBonus(_manaRegenerationBonus),
                    Stat.HealthRegeneration => new HealthRegenerationBonus(_healthRegenerationBonus),
                    Stat.Mana => new ManaBonus(_manaBonus),
                    Stat.Evasion => new EvasionBonus(_evasionBonus),
                    Stat.Accuracy => new AccuracyBonus(_accuracyBonus),
                    _ => throw new ArgumentOutOfRangeException(nameof(stats), stats, null)
                };
            }


            return stat.Select(BonusTo);
        }
        
        public override string GetInfoDisplayText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Rarity.Name).AppendLine().AppendLine();
            
            if (_damageBonus != 0)
            {
                stringBuilder.Append("Damage: ").Append(_damageBonus).AppendLine();
            }

            if (_criticalChance != 0)
            {
                stringBuilder.Append("Cr. chance: ").Append(_criticalChance).AppendLine();
            }

            if (_criticalDamage != 0)
            {
                stringBuilder.Append("Cr. damage: ").Append(_criticalDamage).AppendLine();
            }

            if (_accuracyBonus != 0)
            {
                stringBuilder.Append("Accuracy: ").Append(_accuracyBonus).AppendLine();
            }

            if (_attackSpeedBonus != 0)
            {
                stringBuilder.Append("At. speed: ").Append(_attackSpeedBonus).AppendLine();
            }
            
            stringBuilder.Append(Description).AppendLine();
            stringBuilder.Append("Sell price: ").Append(SellPrice).AppendLine();
            
            stringBuilder.AppendLine();
            /*foreach (var damage in _additionalDamage)
            {
                switch (damage.Key)
                {
                    case DamageType.Fire:
                        if (damage.Value != 0)
                            stringBuilder.Append("<color=#FF8560>").Append("Fire: ").Append(damage.Value).Append("</color>").AppendLine();
                        break;
                    case DamageType.Ice:
                        if (damage.Value != 0)
                            stringBuilder.Append("<color=#B8F3FF>").Append("Ice: ").Append(damage.Value).Append("</color>").AppendLine();
                        break;
                    case DamageType.Physical:
                        if (damage.Value != 0)
                            stringBuilder.Append("<color=#C1C1C1>").Append("Physical: ").Append(damage.Value).Append("</color>").AppendLine();
                        break;
                    case DamageType.Ground:
                        if (damage.Value != 0)
                            stringBuilder.Append("<color=#7B432C>").Append("Ground: ").Append(damage.Value).Append("</color>").AppendLine();
                        break;
                    case DamageType.Thunder:
                        if (damage.Value != 0)
                            stringBuilder.Append("<color=#B8F3FF>").Append("Thunder: ").Append(damage.Value).Append("</color>").AppendLine();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }*/
            
            return stringBuilder.ToString();
        }
    }
}