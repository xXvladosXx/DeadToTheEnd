using Data.Stats.Core;
using GameCore;
using GameCore.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class CharacteristicApplierUI : UIElement
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _unassignedPoints;
        
        private StatsValueStorage _statsValueStorage;

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _statsValueStorage = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer.StatsValueStorage;
            _button.onClick.AddListener(ApplyCharacteristicDistribution);
            _unassignedPoints.text = _statsValueStorage.UnassignedPoints.ToString();
            _statsValueStorage.OnStatsChange += ChangePointsChange;
        }

        private void ChangePointsChange()
        {
            _unassignedPoints.text = _statsValueStorage.UnassignedPoints.ToString();
        }

        private void ApplyCharacteristicDistribution()
        {
            _statsValueStorage.Confirm();
        }
    }
}