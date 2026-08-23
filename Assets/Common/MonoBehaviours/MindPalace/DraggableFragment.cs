using System;
using System.Collections;
using Common.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Common.MonoBehaviours.MindPalace
{
    public class DraggableFragment : MonoBehaviour
    {
        private Vector2 _fragmentPosition;
        private bool _isAnimating;
        private bool _isBeingDragged;

        [Header("Wrappers")]
        [SerializeField] private FragmentDropSlotWrapper mousedOverSlot;
        [SerializeField] private DraggableFragmentWrapper mousedOverFragment;

        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset inputActions;
        private InputAction _pointAction;
        private InputAction _clickAction;
        
        [Header("Fragment Animation")]
        [SerializeField] private float snapSpeedTime;
        
        [Header("Fragment Components")]
        [SerializeField] private Image glowImage;
        private RectTransform _rectTransform;

        private void Awake()
        {
            Debug.Assert(mousedOverSlot != null, nameof(mousedOverSlot) + " != null");
            Debug.Assert(mousedOverFragment != null, nameof(mousedOverFragment) + " != null");
            Debug.Assert(inputActions != null, nameof(inputActions) + " != null");
            Debug.Assert(glowImage != null, nameof(glowImage) + " != null");

            _rectTransform = GetComponent<RectTransform>();
            
            _pointAction = inputActions["UI/Point"];
            _clickAction = inputActions["UI/Click"];

            mousedOverFragment.Changed += SetGlow;
        }

        private void OnDestroy()
        {
            mousedOverFragment.Changed += SetGlow;
        }

        private void Update()
        {
            if (_isBeingDragged) UpdateDrag();
        }

        private void MoveToPosition()
        {
            StopAllCoroutines();
            _isAnimating = false;

            StartCoroutine(AnimateToPosition());
        }
        
        private void MoveToPosition(Vector2 newPosition)
        {
            _fragmentPosition = newPosition;
            
            MoveToPosition();
        }

        private IEnumerator AnimateToPosition()
        {
            _isAnimating = true;

            var positionInitial = _rectTransform.anchoredPosition;
            var timePassed = 0f;

            while (_rectTransform.anchoredPosition != _fragmentPosition)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(
                    positionInitial,
                    _fragmentPosition,
                    timePassed / snapSpeedTime
                );
                
                timePassed += Time.deltaTime;
                yield return null;
            }

            _isAnimating = false;
        }

        public void DestroyFragment()
        {
            Destroy(gameObject);
        }

        public void StartDrag()
        {
            if (_isAnimating) return;

            _clickAction.canceled += EndDrag;

            _isBeingDragged = true;
        }

        private void UpdateDrag()
        {
            var pointPosition = _pointAction.ReadValue<Vector2>();
    
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                pointPosition,
                null, // null is used when canvas is set to screen space overlay
                out var localPoint);

            _rectTransform.anchoredPosition = localPoint;
        }

        private void EndDrag(InputAction.CallbackContext ctx)
        {
            _clickAction.canceled -= EndDrag;

            if (!_isBeingDragged) return;
            _isBeingDragged = false;

            var slot = mousedOverSlot.CurrentDropSlot;
            if (slot != null) { MoveToPosition(slot.GetComponent<RectTransform>().anchoredPosition); }
            else MoveToPosition();
            
            mousedOverSlot.Set(null);
        }
        
        private void SetGlow()
        {
            glowImage.enabled = mousedOverFragment.CurrentFragment == this;
        }
    }
}