using System;
using System.Collections.Generic;
using System.Linq;
using GameCore;
using GameCore.Player;
using SkillsSystem;
using SkillsSystem.SkillBonuses;
using UnityEngine;

namespace UI.Bonuses
{
    public class BonusesUI : UIElement
    {
        [SerializeField] private BonusSlotUI _bonusSlotUI;
        [SerializeField] private int _size;
        
        private BuffManager _buffManager;
        private List<BonusSlotUI> _bonusSlotUis = new List<BonusSlotUI>();

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _buffManager = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.BuffManager;
            for (int i = 0; i < _size; i++)
            {
                var bonusUI = Instantiate(_bonusSlotUI, transform);
                _bonusSlotUis.Add(bonusUI);
            }

            _buffManager.OnBonusAdded += OnBonusAdded;
        }

        private void Update()
        {
            foreach (var bonusSlotUi in _bonusSlotUis)
            {
                if(!bonusSlotUi.gameObject.activeSelf) continue;
                bonusSlotUi.UpdateCooldown(_buffManager);
            }
        }

        private void OnBonusAdded(StatBonus statBonus)
        {
            if (statBonus is StatTimeableBonus timeableBonus)
            {
                _bonusSlotUis.FirstOrDefault(slot => !slot.HasBonus)?.SetBonusInfo(timeableBonus);
            }
        }
    }
}