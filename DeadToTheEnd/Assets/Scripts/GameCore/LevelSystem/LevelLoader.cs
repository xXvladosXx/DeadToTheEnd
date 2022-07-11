using System;
using System.Collections;
using System.Threading.Tasks;
using GameCore.Save;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore.LevelSystem
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _progressBar;

        [field: SerializeField] public float TimeToWait { get; private set; } = 1;

        private SaveInteractor _saveInteractor;
        private string _lastSaveFile;
        public event Action OnFadeStarted;
        
        public void Awake()
        {
            _canvas.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        public void Init(SaveInteractor saveInteractor)
        {
            _saveInteractor = saveInteractor;
        }
        
        public void SaveBeforeAndAfterLoading(int sceneIndex, string saveFile = SaveInteractor.DEFAULT_SAVE_FILE)
        {
            OnFadeStarted?.Invoke();

            SaveBeforeLoading(saveFile, sceneIndex);
            _lastSaveFile = saveFile;
            Debug.Log(_lastSaveFile);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        

        public void SaveBeforeLoading(string saveFile, int sceneIndex)
        {
            _saveInteractor.Save(saveFile);
            StartCoroutine(LoadLevel(sceneIndex, saveFile));
        }
        
        private IEnumerator LoadLevel(int sceneIndex, string saveFile)
        {
            OnFadeStarted?.Invoke();
            yield return new WaitForSeconds(TimeToWait);
           _saveInteractor.Load(saveFile);
            
            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            scene.allowSceneActivation = false;
            
            _canvas.gameObject.SetActive(true);

            do
            {
                _progressBar.fillAmount = scene.progress;
            } while (scene.progress < 0.9f);
            
            _canvas.gameObject.SetActive(false);
            scene.allowSceneActivation = true;
            Destroy(gameObject);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _saveInteractor.Save(_lastSaveFile);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDestroy()
        {
        }
    }
}