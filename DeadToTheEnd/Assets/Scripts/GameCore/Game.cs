using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.SceneSystem;
using UI;
using UI.Controllers;
using Utilities;

namespace GameCore
{
    public sealed class Game
    {
        public static event Action OnGameInitialized;
        
        public static SceneManagerBase SceneManagerBase { get; private set; }

        public static void Run(SceneManagerBase sceneManagerBase, UIController uiController)
        {
            SceneManagerBase = sceneManagerBase;
            Coroutines.StartRoutine(InitializeGameRoutine(uiController));
        }
        private static IEnumerator InitializeGameRoutine(UIController uiController)
        {
            SceneManagerBase.InitScenesConfig();

            yield return SceneManagerBase.LoadCurrentSceneAsync(uiController);
            OnGameInitialized?.Invoke();
        }


        public static T GetInteractor<T>() where T : Interactor => SceneManagerBase.GetInteractor<T>();
        public static T GetRepository<T>() where T : Repository => SceneManagerBase.GetRepository<T>();
        public static Dictionary<Type, Interactor> GetInteractors => SceneManagerBase.GetInteractors;

       
    }
}