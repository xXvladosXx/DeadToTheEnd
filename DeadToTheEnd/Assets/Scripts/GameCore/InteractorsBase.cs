using System;
using System.Collections.Generic;
using GameCore.SceneSystem;

namespace GameCore
{
    public class InteractorsBase
    {
        private Dictionary<Type, Interactor> _interactors;
        private SceneConfig _sceneConfig;

        public InteractorsBase(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
        }

        public void CreateAllInteractors()
        {
            _interactors = _sceneConfig.CreateAllInteractors();
        }

        public void SendOnCreateInteractors()
        {
            var allInteractors = _interactors.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.OnCreate();
            }
        }
        
        public void InitializeInteractors()
        {
            var allInteractors = _interactors.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.Init();
            }
        }
        
        public void SendOnStartInteractors()
        {
            var allInteractors = _interactors.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.OnStart();
            }
        }

        public T GetInteractor<T>() where T : Interactor
        {
            var type = typeof(T);
            return (T) _interactors[type];
        }
        public Dictionary<Type, Interactor> GetInteractors() 
        {
            return _interactors;
        }
    }
}