using System;
using System.Collections.Generic;
using Entities;
using InventorySystem;
using UnityEngine;
using Utilities.Raycast;

namespace LootSystem
{
    public class InteractableChecker : MonoBehaviour
    {
        private MainPlayer _mainPlayer;
        private IInteractable _lastInteractableObject;
        
        public IInteractable CurrentInteractableObject { get; set; }
        
        public event Action<ItemContainer> OnLootOpen;
        public event Action<IInteractable> OnInteractableRequest;
        public event Action OnInteractableHide;

        private void Awake()
        {
            _mainPlayer = GetComponent<MainPlayer>();
        }

        private void Update()
        {
            if(CurrentInteractableObject != null) return;
            
            Transform target = RaycastChecker.CheckRaycastExcept(_mainPlayer.MainCamera.position,
                _mainPlayer.MainCamera.forward, Mathf.Infinity, _mainPlayer.PlayerLayerData.PlayerLayer);
            
            if(target == null) return;
            
            if (target.TryGetComponent(out IInteractable interactable))
            {
                if(Vector3.Distance(gameObject.transform.position, target.transform.position) 
                   > interactable.GetDistanceOfRaycast)
                    return;
                
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    CurrentInteractableObject = interactable;
                    TryToInteract(interactable);
                }
                
                if (interactable == _lastInteractableObject) return;
                
                _lastInteractableObject = interactable;
                OnInteractableRequest?.Invoke(interactable);
            }
            else
            {
                OnInteractableHide?.Invoke();
                _lastInteractableObject = null;
            }
        }

        private void TryToInteract(IInteractable interactable)
        {
            switch (interactable)
            {
                case PickableObject:
                    var loot = (ItemContainer)interactable.ObjectOfInteraction();
                    OnLootOpen?.Invoke(loot);
                    
                    break;
            }
        }
    }
}