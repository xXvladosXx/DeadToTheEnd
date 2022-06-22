using System;
using UnityEngine;

namespace SaveSystem
{
    public class LoadedObjects : MonoBehaviour
    {
        [SerializeField] private GameObject _persistentData;

        private static bool _hasSpawned;

        private void Awake()
        {
            if(_hasSpawned) return;
            
            SpawnPersistentObjects();
            _hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            var persistentObject = Instantiate(_persistentData);
            DontDestroyOnLoad(persistentObject);
        }
    }
}