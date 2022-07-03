using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI.Inventory.ItemContainers
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
        protected virtual void Update()
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