using System;
using GameCore;
using GameCore.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Rage
{
    public class RageUI : UIElement
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _rageText;

        private Animator _animator;
        private StatsSystem.Rage _rage;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _animator = GetComponent<Animator>();
            
            _rage = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.Rage;
            _rage.OnValuePctChanged += ChangeRageValuePct;
            
            _image.fillAmount = 0;
            _rageText.text = string.Empty;
        }

        private void ChangeRageValuePct(float pct)
        {
            _image.fillAmount = pct;
            
            _rageText.text = (pct * 100).ToString();
            _animator.Play("Rage");
        }
    }
}