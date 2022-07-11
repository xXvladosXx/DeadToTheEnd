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
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TextMeshProUGUI _priceText;

        private ShopInteractor _shopInteractor;
        private ItemSlot _itemSlot;
        
        private int _currentQuantity;
        private bool _isBuying;
        
        public void Init(ShopInteractor shopInteractor, ItemSlot itemSlot, bool buy)
        {
            _shopInteractor = shopInteractor;
            _itemSlot = itemSlot;
            
            gameObject.SetActive(true);
            
            _slider.minValue = 1;
            _slider.maxValue = itemSlot.Quantity;
            _slider.value = 1;
            
            _amountText.text = 1.ToString();
            _currentQuantity = 1;
            _isBuying = buy;

            _infoText.text = buy ? $"You want to buy {itemSlot.Item.name}" : $"You want to sell {itemSlot.Item.name}";
            
            _priceText.text = (itemSlot.Item.SellPrice * _currentQuantity).ToString();

            if (_isBuying)
            {
                print("isBuying");
                _accept.interactable = _shopInteractor.HasAllRequirements(_itemSlot, _currentQuantity);
            }
        }

        private void SetAmountText(float arg0)
        {
            _currentQuantity = Mathf.RoundToInt(arg0);
            _amountText.text = _currentQuantity.ToString();
            _priceText.text = (_itemSlot.Item.SellPrice * _currentQuantity).ToString();
            
            if(_isBuying)
                _accept.interactable = _shopInteractor.HasAllRequirements(_itemSlot, _currentQuantity);
        }

        private void OnEnable()
        {
            _accept.onClick.AddListener(AcceptTransition);
            _reject.onClick.AddListener(RejectTransition);
            _slider.onValueChanged.AddListener(SetAmountText);
        }

        private void AcceptTransition()
        {
            gameObject.SetActive(false);

            _shopInteractor.StartTransition(_itemSlot, _isBuying, _currentQuantity);
        }

        private void RejectTransition()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveAllListeners();
            _reject.onClick.RemoveAllListeners();
            _slider.onValueChanged.RemoveAllListeners();
        }
    }
}