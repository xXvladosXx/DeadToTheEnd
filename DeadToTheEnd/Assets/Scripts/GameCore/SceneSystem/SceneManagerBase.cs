using System;
using System.Collections;
using System.Collections.Generic;
using UI;
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
        
        public event Action<Scene> OnSceneLoadedEvent; 


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
            return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config, uiController));
        }
        
        public Coroutine LoadNewSceneAsync(string sceneName, UIController uiController)
        {
            if (IsLoading)
            {
                Debug.Log("Loading");
            }

            var config = SceneConfigs[sceneName];
            return Coroutines.StartRoutine(LoadNewSceneRoutine(config, uiController));
        }

        private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig, UIController uiController)
        {
            IsLoading = true;

            yield return Coroutines.StartRoutine(InitializeSceneAsync(sceneConfig, uiController));
            
            IsLoading = false;
            OnSceneLoadedEvent?.Invoke(Scene);
        }
        
        private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig, UIController uiController)
        {
            IsLoading = true;

            yield return Coroutines.StartRoutine(LoadSceneRoutine(sceneConfig));
            yield return Coroutines.StartRoutine(InitializeSceneAsync(sceneConfig, uiController));
            
            IsLoading = false;
            OnSceneLoadedEvent?.Invoke(Scene);
        }
        
        IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
        {
            var async = SceneManager.LoadSceneAsync(sceneConfig.SceneName);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                yield return null;
            }

            async.allowSceneActivation = true;
        }
        
        IEnumerator InitializeSceneAsync(SceneConfig sceneConfig, UIController uiController)
        {
            Scene = new Scene(sceneConfig);
            yield return Scene.InitializeAsync(uiController);
        }

        public T GetRepository<T>() where T : Repository => Scene.GetRepository<T>();
        public T GetInteractor<T>() where T : Interactor => Scene.GetInteractor<T>();
    }
}