using GameCore.Player;
using GameCore.Save;
using SaveSystem;
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
            
            _saveInteractor.AddEntity(LevelExitor);
            _saveInteractor.AddEntity(LevelCompleter.Instance);
        }

        public void LoadLevel(int index, string saveFile = SaveInteractor.DEFAULT_SAVE_FILE)
        {
            LevelLoader.SaveBeforeAndAfterLoading(index, saveFile);            
        }

        public void LoadLevelWithSave(int index, string saveFile = SaveInteractor.DEFAULT_SAVE_FILE)
        {
            LevelLoader.SaveBeforeAndAfterLoading(index, saveFile);
        }
        public void LoadLevelBeforeSave(int index, string saveFile = SaveInteractor.DEFAULT_SAVE_FILE)
        {
            LevelLoader.SaveBeforeLoading(saveFile, index);
        }
    }
}