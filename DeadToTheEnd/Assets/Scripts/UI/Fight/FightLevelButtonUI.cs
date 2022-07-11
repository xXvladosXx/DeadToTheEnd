using System;
using GameCore;
using GameCore.LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fight
{
    [RequireComponent(typeof(Button))]
    public class FightLevelButtonUI : MonoBehaviour
    {
        [SerializeField] private Image _completedLevel;
        
        private Button _levelLoader;
        private LevelLoaderInteractor _levelLoaderInteractor;

        private int _levelIndex;

        private void Awake()
        {
            _levelLoader = GetComponent<Button>();
            _completedLevel.gameObject.SetActive(false);
        }
      
        public void SetData(int levelIndex, LevelLoaderInteractor levelLoaderInteractor)
        {
            _levelLoader.onClick.RemoveAllListeners();
            _levelIndex = levelIndex;
            _levelLoader.onClick.AddListener(() => LoadLevel(_levelIndex));
            
            _levelLoaderInteractor = levelLoaderInteractor;
            foreach (var completedLevel in LevelCompleter.Instance.CompletedLevels)
            {
                Debug.Log(completedLevel);
                _completedLevel.gameObject.SetActive(completedLevel == _levelIndex);
            }
        }

        private void LoadLevel(int index)
        {
            _levelLoaderInteractor.LoadLevelBeforeSave(index);
        }
    }
}