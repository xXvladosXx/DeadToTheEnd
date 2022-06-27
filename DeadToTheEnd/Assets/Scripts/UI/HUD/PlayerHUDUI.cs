using GameCore;
using GameCore.Player;
using UI.Stats;
using UnityEngine;

namespace UI.HUD
{
    public class PlayerHUDUI : UIElement
    {
        [SerializeField] private BarUI[] _barUis;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            Show();
            var mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            foreach (var barUi in _barUis)
            {
                barUi.InitBarData(mainPlayer);
            }            
        }
    }
}