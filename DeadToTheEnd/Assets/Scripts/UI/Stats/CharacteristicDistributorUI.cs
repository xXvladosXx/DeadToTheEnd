using System;
using Data.Stats;
using Data.Stats.Core;
using GameCore;
using GameCore.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class CharacteristicDistributorUI : UIElement
    {
        [SerializeField] private Button _minus;
        [SerializeField] private Button _plus;
        [SerializeField] private Characteristic _characteristic;
        [SerializeField] private TextMeshProUGUI _valueText;
        
        private StatsValueStorage _statsValueStorage;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();

            _statsValueStorage = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.StatsValueStorage;
            _plus.onClick.AddListener(IncreaseCharacteristic);
            _minus.onClick.AddListener(DecreaseCharacteristic);
        }
        private void Update()
        {
            if(_statsValueStorage == null) return;
            
            _minus.interactable = _statsValueStorage.CanAssignPoints(_characteristic, -1);
            _plus.interactable = _statsValueStorage.CanAssignPoints(_characteristic, 1);

            _valueText.text = _statsValueStorage.GetProposedPoints(_characteristic).ToString();
        }

        private void IncreaseCharacteristic()
        {
            Allocate(1);
        }

        private void DecreaseCharacteristic()
        {
            Allocate(-1);
        }

        private void Allocate(int points)
        {
            _statsValueStorage.AssignPoints(_characteristic, points);
        }
    }
}