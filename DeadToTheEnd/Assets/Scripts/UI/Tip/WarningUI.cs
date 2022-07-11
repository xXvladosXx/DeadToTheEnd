using System;
using GameCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tip
{
    public class WarningUI : MonoBehaviour
    {
        public static WarningUI Instance { get; private set; }
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _yes;
        [SerializeField] private Button _no;

        public event Action OnAccepted;
        public event Action OnRejected;

        private void Awake()
        {
            Instance = this;
            Hide();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _yes.onClick.AddListener(() =>
            {
                OnAccepted?.Invoke();
                Hide();
            });
            _no.onClick.AddListener(() =>
            {
                OnRejected?.Invoke();
                Hide();
            });
        }

        private void OnDisable()
        {
            _yes.onClick.RemoveAllListeners();
            _no.onClick.RemoveAllListeners();
        }

        public void ShowWarning(string text)
        {
            Show();
            _text.text = text;
        }
    }
}