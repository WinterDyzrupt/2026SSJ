using System.Collections.Generic;
using Common.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Common.MonoBehaviours.MindPalace
{
    public class PalaceMouseController : MonoBehaviour
    {
        [Header("Wrappers")]
        [SerializeField] private BoolWrapper isMindPalaceActive;
        [SerializeField] private MonoBehaviourWrapper mousedOverFragment;
        [SerializeField] private MonoBehaviourWrapper mousedOverSlot;
        
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset inputActions;
        private InputAction _pointAction;
        private InputAction _clickAction;

        private bool _draggingFragment;
        
        private void Awake()
        {
            Debug.Assert(isMindPalaceActive != null, nameof(isMindPalaceActive) + " != null");
            Debug.Assert(inputActions != null, nameof(inputActions) + " != null");
            
            inputActions.Enable();
            
            _pointAction = inputActions["UI/Point"];
            _clickAction = inputActions["UI/Click"];
            
            _clickAction.started += ClickStarted;
            _clickAction.canceled += ClickCanceled;
        }

        private void OnDestroy()
        {
            _clickAction.started -= ClickStarted;
            _clickAction.canceled -= ClickCanceled;
        }

        private void Update()
        {
            if (!isMindPalaceActive) return;
            
            UpdateHover();
        }

        private void UpdateHover()
        {
            var mousePos = _pointAction.ReadValue<Vector2>();
            
            if (!_draggingFragment)
            {
                var hitFragmentResults = UIRaycast<DraggableFragment>(mousePos);
                mousedOverFragment.Set(hitFragmentResults);
            }
            else
            {
                var hitSlotResults = UIRaycast<FragmentDropSlot>(mousePos);
                mousedOverSlot.Set(hitSlotResults);
            }
        }
        
        private void ClickStarted(InputAction.CallbackContext ctx)
        {
            if (!isMindPalaceActive)
            {
                Debug.LogWarning("ClickStarted was attempted but MindPalace shouldn't be active.");
                return;
            }
            
            UpdateHover();

            if (mousedOverFragment.Current is DraggableFragment fragment)
            {
                fragment.StartDrag();
                _draggingFragment = true;
                mousedOverFragment.Set(null);
            }
        }

        private void ClickCanceled(InputAction.CallbackContext ctx)
        {
            if (!isMindPalaceActive) return;
            
            _draggingFragment = false;
        }

        private static T UIRaycast<T>(Vector2 screenPos) where T : MonoBehaviour
        {
            var results = new List<RaycastResult>();
            var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };
            EventSystem.current.RaycastAll(pointerData, results); // Grabbing all objects hit under the pointer
            
            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent<T>(out var component))
                {
                    return component;
                }
            }
            
            return null;
        }
    }
}