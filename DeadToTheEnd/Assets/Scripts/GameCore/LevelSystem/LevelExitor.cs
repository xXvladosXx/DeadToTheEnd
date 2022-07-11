using System;
using System.Collections.Generic;
using Entities;
using GameCore.SaveSystem;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.LevelSystem
{
    public class LevelExitor : SavableEntity
    {
        [SerializeField] private int _defaultSceneIndex;

        private LevelLoader _levelLoader;

        
        public void Init(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MainPlayer mainPlayer))
            {
                LevelCompleter.Instance.LevelCompleted(SceneManager.GetActiveScene().buildIndex);
                _levelLoader.SaveBeforeAndAfterLoading(_defaultSceneIndex);
            }
        }

    }
}