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
        public const string DEFAULT_SAVE_FILE = "QuickSave";

        private SaveRepository _repository;

        public event Action OnGameReloaded;
        public event Action<int, string> OnSceneLoadRequest;
        public event Action<int, string> OnStartSceneLoadRequest;

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

        public void ContinueGame(string saveFile = DEFAULT_SAVE_FILE)
        {
            saveFile = Path.GetFileNameWithoutExtension(saveFile);

            Coroutines.StartRoutine(LoadScene(saveFile));
        }

        public void LoadGame(bool withSave, string saveFile = DEFAULT_SAVE_FILE)
        {
            Coroutines.StartRoutine(LoadScene(saveFile));
        }
        
        private IEnumerator LoadScene(string saveFile)
        {
            var sceneIndex = _repository.GetSceneIndex(saveFile);
            
            OnSceneLoadRequest?.Invoke(sceneIndex, saveFile);
            
            yield return _repository.LoadScene(saveFile);
        }

        private void LoadStartScene(string saveFile)
        {
            OnStartSceneLoadRequest?.Invoke(1, saveFile);
        }
        
        public void Load(string saveFile = DEFAULT_SAVE_FILE)
        {
            _repository.Load(saveFile);
            
            OnGameReloaded?.Invoke();
        }

        public void LoadFromLastSave(string saveInteractorGetLastSave)
        {
            _repository.Load(Path.GetFileNameWithoutExtension(saveInteractorGetLastSave));
            
            OnGameReloaded?.Invoke();
        }
        public void Save(string saveFile = DEFAULT_SAVE_FILE) => _repository.Save(saveFile);
        public void SaveSettings(SettingsSaveData settingsSaveData) => _repository.SaveSettings(settingsSaveData);
        public SettingsSaveData LoadSetting() => _repository.LoadSettings();
        public IEnumerable<string> SaveList() => _repository.SavesList();

        public int GetSceneIndex(string saveFile) => _repository.GetSceneIndex(saveFile);
        public string GetLastSave => Directory.GetFiles(Path.Combine(Application.persistentDataPath))
            .Select(x => new FileInfo(x))
            .OrderByDescending(x => x.LastWriteTime)
            .FirstOrDefault()
            ?.ToString();
        
    }
    
    [Serializable]
    public class SettingsSaveData
    {
        public float EffectsVolume;
        public float MusicVolume;
        public int ResolutionIndex;
        public bool Fullscreen;
        public int GraphicsIndex;
    }
}