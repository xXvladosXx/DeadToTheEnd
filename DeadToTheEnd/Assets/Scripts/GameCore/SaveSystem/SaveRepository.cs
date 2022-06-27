using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.Save
{
    public class SaveRepository : Repository
    {
        public List<SavableEntity> SavableEntities { get; private set; } = new List<SavableEntity>();

        public override void Init()
        {
        }

        public override void OnStart()
        {
            
        }

        public override void OnCreate()
        {
            
        }
        
        public void Save(string saveFile)
        {
            Dictionary<string, object> capturedStates = LoadFile(saveFile);
            CaptureState(capturedStates);
            SaveFile(saveFile, capturedStates);
        }

        private void CaptureState(Dictionary<string, object> capturedStates)
        {
            foreach (var savableEntity in SavableEntities)
            {
                if(savableEntity == null) continue;
                
                capturedStates[savableEntity.GetUniqueIdentifier()] = savableEntity.CaptureState();
            }

            capturedStates["SceneIndexToLoad"] = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(capturedStates["SceneIndexToLoad"]);
        }

        private void SaveFile(string saveFile, object captureState)
        {
            string path = GetPathFromSaveFile(saveFile);

            Type type = captureState.GetType();
            Debug.Log("Saving tp " + path + " " + saveFile);

            using (FileStream fileStream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                if (!type.IsSerializable) return;

                formatter.Serialize(fileStream, captureState);
            }
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            Debug.Log("Load from " + saveFile);
            
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }

            using FileStream fileStream = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>) formatter.Deserialize(fileStream);
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var savableEntity in SavableEntities)
            {
                if(savableEntity == null) continue;
                string uniqueID = savableEntity.GetUniqueIdentifier();

                if (state.ContainsKey(uniqueID))
                {
                    savableEntity.RestoreState(state[uniqueID]);
                }
            }
        }

        public IEnumerator LoadScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
           
            yield return null;

            RestoreState(state);
        }


        public IEnumerable<string> SavesList()
        {
            foreach (var save in Directory.EnumerateFiles(Application.persistentDataPath))
            {
                if (Path.GetExtension(save) == ".sav")
                    yield return Path.GetFileNameWithoutExtension(save);
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
        
        public int GetSceneIndex(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            if (state.ContainsKey("SceneIndexToLoad"))
            {
                int buildIndex = (int) state["SceneIndexToLoad"];

                return buildIndex;
            }

            return -1;
        }
    }
}