using System;
using Data.Stats;
using Entities.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class LevelBarUI : BarUI
    {
        [SerializeField] private TextMeshProUGUI[] _levelText;

        private LevelCalculator _levelCalculator;
        
        public override void InitBarData(AliveEntity aliveEntity)
        {
            SetLevelToUI(aliveEntity.LevelCalculator);
            _levelCalculator = aliveEntity.LevelCalculator;

            Bar.fillAmount = _levelCalculator.GetExpPct;
            
            _levelCalculator.OnLevelUp += RefreshLevel;
            _levelCalculator.OnExperiencePctChanged += RefreshExperience;
        }

        private void RefreshExperience(float obj)
        {
            Bar.fillAmount = obj;
        }

        private void RefreshLevel(int obj)
        {
            SetLevelToUI(_levelCalculator);
        }
        
        private void SetLevelToUI(LevelCalculator levelCalculator)
        {
            foreach (var text in _levelText)
            {
                text.text = levelCalculator.Level.ToString();
            }
        }

        private void OnDisable()
        {
            _levelCalculator.OnLevelUp -= RefreshLevel;
        }

        
    }
}