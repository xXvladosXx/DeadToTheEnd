using System;
using GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fight
{
    public class LevelCompleterUI : MonoBehaviour
    {
        [SerializeField] private Image _foreground;

        private int _allLevels;
        private int _completedLevels;

        public void Init(int allLevels)
        {
            _allLevels = allLevels;
            _completedLevels = LevelCompleter.Instance.CompletedLevels.Count - 1;

            _foreground.fillAmount = (float) _completedLevels / _allLevels;
        }
    }
}