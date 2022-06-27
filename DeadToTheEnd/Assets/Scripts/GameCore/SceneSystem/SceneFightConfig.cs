using System;
using System.Collections.Generic;
using GameCore.GameSystem;
using GameCore.LevelSystem;
using GameCore.Player;
using GameCore.Save;
using GameCore.ShopSystem;

namespace GameCore.SceneSystem
{
    public class SceneFightConfig  : SceneConfig
    {
        public const string SCENE_NAME = "TestScene";

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositories = new Dictionary<Type, Repository>();
            CreateRepository<SaveRepository>(repositories);

            return repositories;
        }

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactors = new Dictionary<Type, Interactor>();
            CreateInteractor<SaveInteractor>(interactors);
            CreateInteractor<PlayerInteractor>(interactors);
            CreateInteractor<LevelLoaderInteractor>(interactors);
            CreateInteractor<GameStateInteractor>(interactors);
            CreateInteractor<EntitySpawnerInteractor>(interactors);

            return interactors;
        }

        public override string SceneName => SCENE_NAME;
    }
}