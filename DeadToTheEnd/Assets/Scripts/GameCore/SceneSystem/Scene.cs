using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Player;
using UI;
using UI.Controllers;
using UnityEngine;
using Utilities;

namespace GameCore.SceneSystem
{
    public class Scene
    {
        private InteractorsBase _interactorsBase;
        private RepositoriesBase _repositoriesBase;
        private SceneConfig _sceneConfig;

        public Scene(SceneConfig config)
        {
            _sceneConfig = config;
            _interactorsBase = new InteractorsBase(config);
            _repositoriesBase = new RepositoriesBase(config);
        }

        public Coroutine InitializeAsync(UIController uiController) => Coroutines.StartRoutine(InitializeRoutine(uiController));
        public T GetRepository<T>() where T : Repository => _repositoriesBase.GetRepository<T>();

        public T GetInteractor<T>() where T : Interactor => _interactorsBase.GetInteractor<T>();
        public Dictionary<Type, Interactor> GetInteractors => _interactorsBase.GetInteractors();
        
        private IEnumerator InitializeRoutine(UIController uiController)
        {
            _repositoriesBase.CreateAllRepositories();
            _interactorsBase.CreateAllInteractors();
            yield return null;

            _repositoriesBase.SendOnCreateRepositories();
            _interactorsBase.SendOnCreateInteractors();
            uiController.SendMessageOnCreate(_interactorsBase);
            yield return null;

            _repositoriesBase.InitializeRepositories();
            _interactorsBase.InitializeInteractors();
            uiController.SendMessageOnInitialize(_interactorsBase);
            yield return null;

            _repositoriesBase.SendOnStartRepositories();
            _interactorsBase.SendOnStartInteractors();
            uiController.SendMessageOnStart(_interactorsBase);
        }
    }
}