using System;
using System.Collections.Generic;
using GameCore.SceneSystem;

namespace GameCore
{
    public class RepositoriesBase
    {
        private Dictionary<Type, Repository> _repositories;
        private SceneConfig _sceneConfig;
        
        public RepositoriesBase(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
        }

        public void CreateAllRepositories()
        {
            _repositories = _sceneConfig.CreateAllRepositories();
        }

       
        public void SendOnCreateRepositories()
        {
            var allRepositories = _repositories.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnCreate();
            }
        }
        
        public void InitializeRepositories()
        {
            var allRepositories = _repositories.Values;
            foreach (var repository in allRepositories)
            {
                repository.Init();
            }
        }
        
        public void SendOnStartRepositories()
        {
            var allRepositories = _repositories.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnStart();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T) _repositories[type];
        }
    }
}