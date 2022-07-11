using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;

[RequireComponent(typeof(LevelSaver))]
public class LevelCompleter : SavableEntity
{
    public static LevelCompleter Instance { get; private set; }
    private LevelSaver _levelSaver;
    public List<int> CompletedLevels => _levelSaver.CompletedLevels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        _levelSaver = GetComponent<LevelSaver>();
    }


    public void LevelCompleted(int buildIndex)
    {
        if(CompletedLevels.Contains(buildIndex)) return;
        
        _levelSaver.AddLevel(buildIndex);
    }
}
