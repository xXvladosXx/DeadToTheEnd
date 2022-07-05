using System;
using SkillsSystem;
using SkillsSystem.SkillBonuses;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bonuses
{
    public class BonusSlotUI : MonoBehaviour
    {
        [SerializeField] private Image _bonusImage;
        [SerializeField] private Image _foreground;

        [SerializeField] private Image _positive;
        [SerializeField] private Image _negative;
        
        public bool HasBonus => _currentStatBonus != null;

        private StatTimeableBonus _currentStatBonus;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetBonusInfo(StatTimeableBonus statBonus)
        {
            _currentStatBonus = statBonus;

            _bonusImage.sprite = statBonus.Sprite;
            
            if (statBonus.Positive)
            {
                _positive.gameObject.SetActive(true);
                _negative.gameObject.SetActive(false);
            }
            else
            {
                _positive.gameObject.SetActive(false);
                _negative.gameObject.SetActive(true);
            }
            
            gameObject.SetActive(true);
        }

        public void UpdateCooldown(BuffManager buffManager)
        {
            var time = buffManager.GetTimeOfBonus(_currentStatBonus);
            _foreground.fillAmount = time;
            
            if (time <= 0)
            {
                _currentStatBonus = null;
                gameObject.SetActive(false);
            }
        }
    }
}