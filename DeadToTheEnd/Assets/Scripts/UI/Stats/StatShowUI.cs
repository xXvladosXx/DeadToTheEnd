using System;
using System.Text;
using Data.Stats;
using Data.Stats.Core;
using GameCore;
using GameCore.Player;
using TMPro;
using UnityEngine;

namespace UI.Stats
{
    public class StatShowUI : UIElement
    {
        [SerializeField] private TextMeshProUGUI _stat;
        
        private StatsFinder _statsFinder;
        private StatsValueStorage _statsValueStorage;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _statsFinder = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.StatsFinder;
            _statsValueStorage = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.StatsValueStorage;
            CreateStats();
        }

        private void CreateStats()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Health: ").Append(_statsFinder.GetStat(Stat.Health)).Append($"<color=#00D100>" +
                    $" {_statsValueStorage.GetCalculatedStat(Stat.Health)}/" + 
                    $"{_statsFinder.GetStat(Stat.Health) - _statsValueStorage.GetCalculatedStat(Stat.Health)}</color>").AppendLine();
            stringBuilder.Append("Health Regeneration: ").Append(_statsFinder.GetStat(Stat.HealthRegeneration)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.HealthRegeneration)}/" + 
                $"{_statsFinder.GetStat(Stat.HealthRegeneration) - _statsValueStorage.GetCalculatedStat(Stat.HealthRegeneration)}</color>").AppendLine();
            stringBuilder.Append("Mana: ").Append(_statsFinder.GetStat(Stat.Mana)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.Mana)}/" + 
                $"{_statsFinder.GetStat(Stat.Mana) - _statsValueStorage.GetCalculatedStat(Stat.Mana)}</color>").AppendLine();
            stringBuilder.Append("Mana Regeneration: ").Append(_statsFinder.GetStat(Stat.ManaRegeneration)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.ManaRegeneration)}/" + 
                $"{_statsFinder.GetStat(Stat.ManaRegeneration) - _statsValueStorage.GetCalculatedStat(Stat.ManaRegeneration)}</color>").AppendLine();
            stringBuilder.Append("Damage: ").Append(_statsFinder.GetStat(Stat.Damage)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.Damage)}/" + 
                $"{_statsFinder.GetStat(Stat.Damage) - _statsValueStorage.GetCalculatedStat(Stat.Damage)}</color>").AppendLine();
            stringBuilder.Append("Critical Damage: ").Append(_statsFinder.GetStat(Stat.CriticalDamage)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.CriticalDamage)}/" + 
                $"{_statsFinder.GetStat(Stat.CriticalDamage) - _statsValueStorage.GetCalculatedStat(Stat.CriticalDamage)}</color>").AppendLine();
            stringBuilder.Append("Critical Chance: ").Append(_statsFinder.GetStat(Stat.CriticalChance)).Append($"<color=#00D100>" +
                $" {_statsValueStorage.GetCalculatedStat(Stat.CriticalChance)}/" + 
                $"{_statsFinder.GetStat(Stat.CriticalChance) - _statsValueStorage.GetCalculatedStat(Stat.CriticalChance)}</color>").AppendLine();

            _stat.text = stringBuilder.ToString();
        }

        private void Update()
        {
            
        }
    }
}