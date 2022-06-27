using Entities;
using GameCore.Save;
using SaveSystem;
using UnityEngine;

namespace GameCore.Player
{
    public class PlayerInteractor : Interactor, ISavableInteractor
    {
        public MainPlayer MainPlayer { get; private set; }
        public override void OnCreate()
        {
            base.OnCreate();
            
            var player = Object.Instantiate(Resources.Load ("Characters/Player") as GameObject);
            //Game.GetInteractor<SaveInteractor>().AddEntity(player.GetComponent<SavableEntity>());

            MainPlayer = player.GetComponentInChildren<MainPlayer>();
        }

        public SavableEntity GetSavableEntity()
        {
            return MainPlayer.GetComponent<SavableEntity>();
        }
    }
}