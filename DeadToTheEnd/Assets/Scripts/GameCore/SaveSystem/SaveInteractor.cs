using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            Coroutines.StartRoutine(LoadStartScene(saveFile));
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
            yield return _repository.LoadScene(saveFile);
        }

        private IEnumerator LoadStartScene(string saveFile)
        {
            yield return SceneManager.LoadSceneAsync(sceneBuildIndex: 3);
            Save(saveFile);
        }
        
        public void Load(string saveFile = _defaultSaveFile)
        {
            _repository.Load(saveFile);
        }

        public void Save(string saveFile = _defaultSaveFile)
        {
            _repository.Save(saveFile);
        }

        public IEnumerable<string> SaveList()
        {
            return _repository.SavesList();
        }

        
        public bool CanSave()
        {
            return true;
        }
    }
}