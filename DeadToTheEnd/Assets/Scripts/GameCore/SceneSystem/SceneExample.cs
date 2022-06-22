using System;
using System.Collections.Generic;
using GameCore.Player;
using GameCore.Save;
using GameCore.ShopSystem;

namespace GameCore.SceneSystem
{
    public class SceneExample : SceneConfig
    {
        public const string SCENE_NAME = "StartScene";
        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositories = new Dictionary<Type, Repository>();
            CreateRepository<SaveRepository>(repositories);
            CreateRepository<ShopRepository>(repositories);

            return repositories;
        }

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactors = new Dictionary<Type, Interactor>();
            CreateInteractor<SaveInteractor>(interactors);
            CreateInteractor<PlayerInteractor>(interactors);
            CreateInteractor<ShopInteractor>(interactors);

            return interactors;
        }

        public override string SceneName => SCENE_NAME;
    }
}