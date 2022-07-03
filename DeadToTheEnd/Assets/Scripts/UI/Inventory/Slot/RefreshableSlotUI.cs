using System;
using Entities;
using GameCore;
using GameCore.Player;
using InventorySystem;
using SkillsSystem;
using StateMachine.Core;
using TimerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RefreshableSlotUI : UIElement
    {
        [SerializeField] private Image _image;
        
        private ItemSlotUI _itemSlotUI;
        private Item _item;
        private ITimeable _timeableItem;
        private MainPlayer _mainPlayer;
        private ITimerController[] _timerControllers;

        private void Awake()
        {
            _itemSlotUI = GetComponent<ItemSlotUI>();
        }

        private void Update()
        {
            _item = _itemSlotUI.Item;

            if(_item == null) return;
            _timeableItem = _itemSlotUI.Item as ITimeable;
            foreach (var timerController in _timerControllers)
            {
                if (timerController.CooldownTimer.GetCooldownValue(_item.GetType()) != 0)
                {
                    var cooldownPct = timerController.CooldownTimer.GetCooldownValue(_item.GetType()) / _timeableItem.GetTime();
                    _image.fillAmount = cooldownPct;
                }
            }
        }

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            _timerControllers = _mainPlayer.GetComponents<ITimerController>();
        }
    }
}