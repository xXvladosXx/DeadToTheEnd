using System;
using Entities;
using UnityEngine;
using Utilities.Raycast;

namespace LootSystem
{
    public class InteractableChecker : MonoBehaviour
    {
        [SerializeField] private float _distanceToPickObject;

        private MainPlayer _mainPlayer;
        private IInteractable _lastInteractableObject;
        
        public event Action<LootContainer> OnLootOpen;

        public event Action<IInteractable> OnInteractableRequest;
        public event Action OnInteractableHide;

        private void Awake()
        {
            _mainPlayer = GetComponent<MainPlayer>();
        }

        private void Update()
        {
            Transform target = RaycastChecker.CheckRaycastExcept(_mainPlayer.MainCamera.position,
                _mainPlayer.MainCamera.forward, _distanceToPickObject, _mainPlayer.PlayerLayerData.PlayerLayer);

            if(target == null) return;
            
            if (target.TryGetComponent(out IInteractable interactable))
            {
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    TryToInteract(interactable);
                }
                
                if (interactable == _lastInteractableObject) return;
                
                _lastInteractableObject = interactable;
                OnInteractableRequest?.Invoke(interactable);
                Debug.Log(interactable);

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
                    var loot = (LootContainer)interactable.ObjectOfInteraction();
                    OnLootOpen?.Invoke(loot);
                    break;
            }
        }
    }
}