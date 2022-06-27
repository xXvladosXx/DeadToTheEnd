using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace GameCore.Save
{
    public class SaveInteractor : Interactor
    {
        private const string _defaultSaveFile = "QuickSave";

        private SaveRepository _repository;

        public event Action OnGameReloaded;
        public event Action<int> OnSceneLoadRequest;
        public event Action<int> OnStartSceneLoadRequest;

        public override void OnCreate()
        {
            base.OnCreate();
            _repository = Game.GetRepository<SaveRepository>();
        }

        public void AddEntity(SavableEntity savableEntity)
        {
            _repository.SavableEntities.Add(savableEntity);
        }
        
        public void RemoveEntity(SavableEntity savableEntity)
        {
            _repository.SavableEntities.Remove(savableEntity);
        }
        
        public void StartNewGame(string saveFile)
        {
          LoadStartScene(saveFile);
        }

        public void ContinueGame(string saveFile = _defaultSaveFile)
        {
            saveFile = Path.GetFileNameWithoutExtension(saveFile);

            Coroutines.StartRoutine(LoadScene(saveFile));
        }

        public void LoadGame(string saveFile = _defaultSaveFile)
        {
            Coroutines.StartRoutine(LoadScene(saveFile));
        }
        
        private IEnumerator LoadScene(string saveFile)
        {
            var sceneIndex = _repository.GetSceneIndex(saveFile);
            OnSceneLoadRequest?.Invoke(sceneIndex);
            
            yield return _repository.LoadScene(saveFile);
        }

        private void LoadStartScene(string saveFile)
        {
            OnStartSceneLoadRequest?.Invoke(1);

            Save(saveFile);
        }
        
        public void Load(string saveFile = _defaultSaveFile)
        {
            _repository.Load(saveFile);
            
            OnGameReloaded?.Invoke();
        }

        public void Save(string saveFile = _defaultSaveFile)
        {
            _repository.Save(saveFile);
        }

        public IEnumerable<string> SaveList() => _repository.SavesList();

        public int GetSceneIndex(string saveFile) => _repository.GetSceneIndex(saveFile);
        public string GetLastSave => Directory.GetFiles(Path.Combine(Application.persistentDataPath))
            .Select(x => new FileInfo(x))
            .OrderByDescending(x => x.LastWriteTime)
            .FirstOrDefault()
            ?.ToString();

    }
}