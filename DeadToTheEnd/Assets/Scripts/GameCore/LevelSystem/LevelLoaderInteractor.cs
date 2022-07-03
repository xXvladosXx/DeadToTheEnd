using GameCore.Save;
using UnityEngine;
using Utilities;

namespace GameCore.LevelSystem
{
    public class LevelLoaderInteractor : Interactor
    {
        private SaveInteractor _saveInteractor;
        public LevelLoader LevelLoader { get; private set; }
        public LevelExitor LevelExitor { get; private set; }
        public override void OnCreate()
        {
            base.OnCreate();
            _saveInteractor = Game.GetInteractor<SaveInteractor>();
            
            LevelLoader = GameObject.Instantiate(Resources.Load("Level/LevelLoader") as GameObject).GetComponent<LevelLoader>();
            LevelExitor = GameObject.Instantiate(Resources.Load("Level/LevelExitor") as GameObject).GetComponent<LevelExitor>();

            _saveInteractor.OnSceneLoadRequest += LoadLevel;
            _saveInteractor.OnStartSceneLoadRequest += LoadLevelWithSave;
            
            LevelLoader.Init(_saveInteractor);
            LevelExitor.Init(LevelLoader);
        }

        public void LoadLevel(int index)
        {
            Coroutines.StartRoutine(LevelLoader.LoadLevel(index));            
        }

        public void LoadLevelWithSave(int index)
        {
            LevelLoader.LoadLevelWithSave(index);
        }
    }
}