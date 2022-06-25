using GameCore.Save;
using UnityEngine;

namespace GameCore.LevelSystem
{
    public class LevelLoaderInteractor : Interactor
    {
        private SaveInteractor _saveInteractor;
        private LevelLoader _levelLoader;
        private LevelExitor _levelExitor;
        public override void OnCreate()
        {
            base.OnCreate();
            _saveInteractor = Game.GetInteractor<SaveInteractor>();
            
            _levelLoader = GameObject.Instantiate(Resources.Load("Level/LevelLoader") as GameObject).GetComponent<LevelLoader>();
            _levelExitor = GameObject.Instantiate(Resources.Load("Level/LevelExitor") as GameObject).GetComponent<LevelExitor>();

            _levelLoader.OnLevelLoaded += () =>
            {
                _saveInteractor.Save();
            };
            
            _levelExitor.Init(_levelLoader);
        }

        public void LoadLevel(int index)
        {
            _levelLoader.LoadLevel(index);            
        }
    }
}