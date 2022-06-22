using System;
using System.Collections.Generic;

namespace GameCore.SceneSystem
{
    public abstract class SceneConfig 
    {
        public abstract Dictionary<Type, Repository> CreateAllRepositories();
        public abstract Dictionary<Type, Interactor> CreateAllInteractors();
        
        public abstract string SceneName { get; }

        public void CreateInteractor<T>(Dictionary<Type, Interactor> interactors) where T : Interactor, new()
        {
            var interactor = new T();
            var type = typeof(T);

            interactors[type] = interactor;
        }
        
        public void CreateRepository<T>(Dictionary<Type, Repository> repositories) where T : Repository, new()
        {
            var repository = new T();
            var type = typeof(T);

            repositories[type] = repository;
        }
    }
}