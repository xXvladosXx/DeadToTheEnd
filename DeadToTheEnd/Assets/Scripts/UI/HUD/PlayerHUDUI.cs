using Entities;
using GameCore;
using GameCore.Player;
using UI.Stats;
using UnityEngine;

namespace UI.HUD
{
    public class PlayerHUDUI : UIElement, IRefreshable
    {
        [SerializeField] private BarUI[] _barUis;
        private MainPlayer _mainPlayer;

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            _mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            foreach (var barUi in _barUis)
            {
                barUi.InitBarData(_mainPlayer);
            }            
        }

        public void Refresh()
        {
            foreach (var barUi in _barUis)
            {
                barUi.InitBarData(_mainPlayer);
            }   
        }
    }
}