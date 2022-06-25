using System;
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

        public event Action OnLevelLoaded;
        public void Awake()
        {
            _canvas.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        public async void LoadLevel(int sceneIndex)
        {
            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            scene.allowSceneActivation = false;
            
            _canvas.gameObject.SetActive(true);

            do
            {
                _progressBar.fillAmount = scene.progress;
                await Task.Delay(100);
                
            } while (scene.progress < 0.9f);
            
            _canvas.gameObject.SetActive(false);

            scene.allowSceneActivation = true;
            OnLevelLoaded?.Invoke();
            
            Destroy(gameObject);
        }
    }
}