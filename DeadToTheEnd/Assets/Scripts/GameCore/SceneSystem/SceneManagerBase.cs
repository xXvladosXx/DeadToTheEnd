using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace GameCore.SceneSystem
{
    public abstract class SceneManagerBase
    {
        public Scene Scene { get; private set; }
        public bool IsLoading { get; private set; }

        protected readonly Dictionary<string, SceneConfig> SceneConfigs;
        
        public SceneManagerBase()
        {
            SceneConfigs = new Dictionary<string, SceneConfig>();
        }

        public abstract void InitScenesConfig();

        public Coroutine LoadCurrentSceneAsync(UIController uiController)
        {
            if (IsLoading)
            {
                Debug.Log("Loading");
            }

            var sceneName = SceneManager.GetActiveScene().name;
            var config = SceneConfigs[sceneName];
            return Coroutines.StartRoutine(InitializeSceneAsync(config, uiController));
        }

        IEnumerator InitializeSceneAsync(SceneConfig sceneConfig, UIController uiController)
        {
            Scene = new Scene(sceneConfig);
            yield return Scene.InitializeAsync(uiController);
        }

        public T GetRepository<T>() where T : Repository => Scene.GetRepository<T>();
        public T GetInteractor<T>() where T : Interactor => Scene.GetInteractor<T>();
        public Dictionary<Type, Interactor> GetInteractors => Scene.GetInteractors;

    }
}