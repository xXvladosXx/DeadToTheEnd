using System;
using GameCore.ShopSystem;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class TransitionRequest : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _accept;
        [SerializeField] private Button _reject;
        [SerializeField] private TextMeshProUGUI _amountText;

        private ShopInteractor _shopInteractor;
        private ItemSlot _itemSlot;
        private int _currentQuantity;
        public void Init(ShopInteractor shopInteractor, ItemSlot itemSlot)
        {
            _shopInteractor = shopInteractor;
            _itemSlot = itemSlot;
            
            gameObject.SetActive(true);
            
            _slider.minValue = 1;
            _slider.maxValue = itemSlot.Quantity;
            _amountText.text = 1.ToString();
            _currentQuantity = 1;

            _slider.onValueChanged.AddListener(SetAmountText);
            
            _accept.interactable = _shopInteractor.HasAllRequirements(_itemSlot, _currentQuantity);
        }

        private void SetAmountText(float arg0)
        {
            _currentQuantity = Mathf.RoundToInt(arg0);
            _amountText.text = _currentQuantity.ToString();
            
            _accept.interactable = _shopInteractor.HasAllRequirements(_itemSlot, _currentQuantity);
        }

        private void OnEnable()
        {
            _accept.onClick.AddListener(AcceptTransition);
            _reject.onClick.AddListener(RejectTransition);
        }

        private void AcceptTransition()
        {
            gameObject.SetActive(false);
            
            _shopInteractor.StartTransition(_itemSlot, false, _currentQuantity);
        }

        private void RejectTransition()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveAllListeners();
            _reject.onClick.RemoveAllListeners();
        }
    }
}