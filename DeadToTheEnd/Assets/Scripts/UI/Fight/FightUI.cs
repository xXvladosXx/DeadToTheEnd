using System.Collections.Generic;
using System.Linq;
using GameCore;
using GameCore.LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fight
{
    [RequireComponent(typeof(LevelCompleterUI))]
    public class FightUI : UIElement, IRefreshable
    {
        [SerializeField] private Transform _content;
        [SerializeField] private FightLevelButtonUI _fightLevel;

        private List<FightLevelButtonUI> _levels;
        private LevelLoaderInteractor _levelLoaderInteractor;
        private LevelCompleterUI _levelCompleterUI;

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _levels = _content.GetComponentsInChildren<FightLevelButtonUI>().ToList();
            _levelLoaderInteractor = interactorsBase.GetInteractor<LevelLoaderInteractor>();
            _levelCompleterUI = GetComponent<LevelCompleterUI>();
            
            SetDataToLevelButtons();
            _levelCompleterUI.Init(_levels.Count);
        }

        public void Refresh()
        {
            SetDataToLevelButtons();
            _levelCompleterUI.Init(_levels.Count);
        }
        
        private void SetDataToLevelButtons()
        {
            var levelIndex = 2;
            foreach (var level in _levels)
            {
                level.SetData(levelIndex, _levelLoaderInteractor);
                levelIndex++;
            }
        }
    }
}