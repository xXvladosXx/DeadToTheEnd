using System;
using System.Collections;
using System.Threading.Tasks;
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
        public event Action OnLevelLoaded;
        public event Action OnFadeStarted;
        
        public void Awake()
        {
            _canvas.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        public void LoadLevelWithSave(int sceneIndex)
        {
            OnLevelLoaded?.Invoke();
            OnFadeStarted?.Invoke();

            StartCoroutine(LoadLevel(sceneIndex));

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        public IEnumerator LoadLevel(int sceneIndex)
        {
            Debug.Log("LevelLoaded");
            OnFadeStarted?.Invoke();
            yield return new WaitForSeconds(TimeToWait);
            
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
            Debug.Log("Current scene is "+SceneManager.GetActiveScene().buildIndex);
            OnLevelLoaded?.Invoke();
        }
    }
}