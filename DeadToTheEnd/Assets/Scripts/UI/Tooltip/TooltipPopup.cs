using System;
using System.Text;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tooltip
{
    public class TooltipPopup : MonoBehaviour
    {
        public static TooltipPopup Instance { get; private set; }
        
        [SerializeField] private Canvas _popupCanvas;
        [SerializeField] private RectTransform _popupObject;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _padding;

        private void Awake()
        {
            Instance = this;
            HideInfo();
        }

        private void Update()
        {
            FollowCursor();
        }

        private void FollowCursor()
        {
            if (!_popupCanvas.gameObject.activeSelf) { return; }

            Vector3 newPos = Input.mousePosition + _offset;
            newPos.z = 0f;
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + _popupObject.rect.width * _popupCanvas.scaleFactor / 2) - _padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }
            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - _popupObject.rect.width * _popupCanvas.scaleFactor / 2) + _padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }
            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + _popupObject.rect.height * _popupCanvas.scaleFactor) - _padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }
            
            _popupObject.transform.position = newPos;
        }

        public void DisplayInfo(Item item)
        {
            if(item == null) return;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<size=35>").Append(item.ColouredName()).Append("</size>").AppendLine();
            stringBuilder.Append("<size=25>").Append(item.GetInfoDisplayText()).Append("</size>");

            _infoText.text = stringBuilder.ToString();
            
            Update();
            _popupCanvas.gameObject.SetActive(true);
            Update();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_popupObject);
        }

        public void HideInfo()
        {
            _popupCanvas.gameObject.SetActive(false);
        }
    }
}