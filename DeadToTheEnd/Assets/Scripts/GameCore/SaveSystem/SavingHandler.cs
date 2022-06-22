/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public class SavingHandler : MonoBehaviour
    {

        private const string _defaultSaveFile = "QuickSave";
        
        public string GetLastSave => Directory.GetFiles(Path.Combine(Application.persistentDataPath))
            .Select(x => new FileInfo(x))
            .OrderByDescending(x => x.LastWriteTime)
            .FirstOrDefault()
            ?.ToString();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartNewGame(string saveFile)
        {
            StartCoroutine(LoadStartScene(saveFile));
        }

        public void ContinueGame(string saveFile = _defaultSaveFile)
        {
            saveFile = Path.GetFileNameWithoutExtension(saveFile);

            StartCoroutine(LoadScene(saveFile));
        }

        public void LoadGame(string saveFile = _defaultSaveFile)
        {
            StartCoroutine(LoadScene(saveFile));
        }

        

        public void Load(string saveFile)
        {
            GetComponent<Saving>().Load(saveFile);
        }

        public void Save(string saveFile)
        {
            GetComponent<Saving>().Save(saveFile);
        }

        public IEnumerable<string> SaveList()
        {
            return GetComponent<Saving>().SavesList();
        }

        public void OverwriteGame(string save)
        {
            GetComponent<Saving>().Save(save);
        }
    }
}*/