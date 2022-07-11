using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.SaveSystem;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaver : MonoBehaviour, ISavable
{
    public List<int> CompletedLevels { get; private set; } = new List<int>();

    public object CaptureState()
    {
        var levelCompleter = new LevelCompleter
        {
            CompletedLevels = CompletedLevels
        };

        return levelCompleter;
    }

    public void RestoreState(object state)
    {
        var levelCompleter = (LevelCompleter) state;
        CompletedLevels = levelCompleter.CompletedLevels;
    }

    public void AddLevel(int buildIndex)
    {
        CompletedLevels.Add(SceneManager.GetActiveScene().buildIndex);
    }
        
    [Serializable]
    public class LevelCompleter 
    {
        public List<int> CompletedLevels;
    }
}
