using UnityEngine;

namespace UI
{
    public abstract class ItemContainerManagerUI : UIElement
    {
        [SerializeField] protected ItemContainerUI[] _itemContainers;

        protected virtual void OnEnable()
        {
            foreach (var itemContainerUI in _itemContainers)
            {
                itemContainerUI.gameObject.SetActive(true);
            }
        }
        private void Update()
        {
            foreach (var itemContainerUI in _itemContainers)
            {
                itemContainerUI.UpdateSlots();
            }
        }
        protected virtual void OnDisable()
        {
            foreach (var itemContainerUI in _itemContainers)
            {
                itemContainerUI.gameObject.SetActive(false);
            }
        }
    }
}