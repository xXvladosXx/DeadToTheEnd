using System;
using Entities;
using UnityEngine;

namespace GameCore.LevelSystem
{
    public class LevelExitor : MonoBehaviour
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
                _levelLoader.LoadLevelWithSave(_defaultSceneIndex);
            }
        }
    }
}