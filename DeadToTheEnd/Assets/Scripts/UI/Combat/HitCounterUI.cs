using System;
using Combat;
using GameCore;
using GameCore.Player;
using TMPro;
using UnityEngine;

namespace UI.Combat
{
    public class HitCounterUI : UIElement
    {
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _hitCount;
        
        private HitCounter _hitCounter;
        private Animator _animator;
        
        private const string START_INCREASE_ANIM = "IncreaseCount";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _hitCounter = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.GetComponent<HitCounter>();
            _hitCounter.OnCounterChanged += ChangeValues;
            _hitCounter.OnCounterReset += Hide;
        }

        private void ChangeValues()
        {
            gameObject.SetActive(true);
            _animator.Play(START_INCREASE_ANIM);
            _hitCount.text = $"{_hitCounter.Counter} HITS";
            _damage.text = $"{_hitCounter.Damage} DAMAGE";
        }
    }
}
