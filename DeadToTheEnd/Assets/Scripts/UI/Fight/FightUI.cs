using System.Collections.Generic;
using System.Linq;
using GameCore;
using GameCore.LevelSystem;
using GameCore.Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FightUI : UIElement
    {
        private List<Button> _levels;
        private LevelLoaderInteractor _levelLoaderInteractor;
        
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _levels = GetComponentsInChildren<Button>().ToList();
            _levelLoaderInteractor = interactorsBase.GetInteractor<LevelLoaderInteractor>();

            var levelIndex = 2;
            foreach (var level in _levels)
            {
                var index = levelIndex;
                level.onClick.AddListener(() => LoadLevel(index));
                levelIndex++;
            }
        }

        private void LoadLevel(int index)
        {
            _levelLoaderInteractor.LoadLevel(index);
        }

    }
}