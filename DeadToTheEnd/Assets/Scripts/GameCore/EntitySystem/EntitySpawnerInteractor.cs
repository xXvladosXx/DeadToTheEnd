using Entities;
using UnityEngine;

namespace GameCore.Player
{
    public class EntitySpawnerInteractor : Interactor
    {
        private MainPlayer _mainPlayer;

        public override void OnCreate()
        {
            base.OnCreate();
            
            var entitySpawner = Object.Instantiate(Resources.Load ("EntitySpawner") as GameObject).GetComponent<EntitySpawner>();
            _mainPlayer = Game.GetInteractor<PlayerInteractor>().MainPlayer;
            entitySpawner.Init(_mainPlayer);
        }
    }
}