using System;
using GameCore;
using GameCore.LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelLoader
{
    public class Fader : UIElement
    {
        [SerializeField] private RectTransform _fader;
        
        private LevelLoaderInteractor _levelLoader;

        private void Awake()
        {
            StartFading();
        }

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _levelLoader = interactorsBase.GetInteractor<LevelLoaderInteractor>();

            Show();
            
            _levelLoader.LevelLoader.OnFadeStarted += StartFadingToScene;
        }

        private void StartFading()
        {
            LeanTween.alpha(_fader, 0, 3).setOnComplete(() =>
            {
                _fader.gameObject.SetActive(false);
            });
        }
        private void StartFadingToScene()
        {
            _fader.gameObject.SetActive(true);
            LeanTween.alpha(_fader, 1, _levelLoader.LevelLoader.TimeToWait).setOnComplete(() =>
            {
                
            });
        }
    }
}