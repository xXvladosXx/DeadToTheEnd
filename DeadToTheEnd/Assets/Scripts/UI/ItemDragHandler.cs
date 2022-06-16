using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class ItemDragHandler: MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _isHovering;
      
        private void OnDisable()
        {
            if (_isHovering)
            {
                _isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                var mouseObj = new GameObject
                {
                    transform =
                    {
                        parent = transform.GetComponentInParent<Canvas>().transform
                    }
                };
               
                MouseData.TempItemDrag = mouseObj;
                
                
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Destroy(MouseData.TempItemDrag);
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (MouseData.TempItemDrag == null) return;

                MouseData.LastItemClicked = MouseData.TempItemDrag;
            
                MouseData.TempItemDrag.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
                MouseData.TempItemDrag.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            MouseData.TempItemHover = gameObject;
            _isHovering = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            MouseData.TempItemHover = null;
            _isHovering = false;
        }
    }

    public static class MouseData
    {
        public static ItemContainerUI UI { get; set; }
        public static GameObject TempItemDrag { get; set; }
        public static GameObject TempItemHover { get; set; }
        public static GameObject LastItemClicked { get; set; }
    }
}