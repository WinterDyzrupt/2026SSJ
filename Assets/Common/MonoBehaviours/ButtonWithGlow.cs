using System.Collections;
using Common.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours
{
    public class ButtonWithGlow : MonoBehaviour
    {
        public Image buttonImage;
        public Image glowImage;
        public Button button;

        public bool canBeInteractedWithMultipleTimes;

        public bool hideWhenNotInteractable;

        /// <summary>
        /// Whether this specific button is interactable.  This is set to false when this button is clicked, preventing
        /// this button from being clicked again.
        /// </summary>
        public BoolWrapper isInteractable;
        
        /// <summary>
        /// Whether a bulk of buttons are interactable.  This is set to false when dialog or the mind palace are visible.
        /// This prevents the bulk of buttons from glowing/etc. when something else is happening.
        /// </summary>
        public BoolWrapper isBulkInteractable;

        [Header("Glow Variables")]
        public Color glowColor;
        [Range(1, 2)] public float glowSize = 1.1f;
        [Range(0, 1)] public float pulseSize = 0.2f;
        [Range(0,1)] public float animateSpeed = 0.4f;

        private bool _isAnimating;
        private Vector3 MinGlowSize => new Vector3(glowSize, glowSize, 0);
        private Vector3 FullPulseSize => new Vector3(glowSize + pulseSize, glowSize + pulseSize, 0);
    
        private void Awake()
        {
            Debug.Assert(buttonImage != null, $"{nameof(buttonImage)} wasn't assigned.");
            Debug.Assert(glowImage != null, $"{nameof(glowImage)} wasn't assigned.");
            Debug.Assert(button != null, $"{nameof(button)} wasn't assigned.");
            Debug.Assert(isInteractable != null, $"{nameof(isInteractable)} wasn't assigned.");
            Debug.Assert(isBulkInteractable != null, $"{nameof(isBulkInteractable)} wasn't assigned.");

            glowImage.sprite = buttonImage.sprite;
            glowImage.color = glowColor;

            isBulkInteractable.Changed += SetInteractable;
            isInteractable.Changed += SetInteractable;

            SetInteractable();
        }

        private void OnDestroy()
        {
            isBulkInteractable.Changed -= SetInteractable;
            isInteractable.Changed -= SetInteractable;
        }

        public void OnClicked()
        {
            if (!canBeInteractedWithMultipleTimes)
            {
                isInteractable.Set(false);
            }
        }

        /// <summary>
        /// Sets this button to be interactable based on the button-specific flag and the bulk-button flag.
        /// </summary>
        private void SetInteractable()
        {
            var isThisButtonInteractable = isInteractable && isBulkInteractable; 

            SetInteractable(isThisButtonInteractable);
        }

        private void SetInteractable(bool value)
        {
            if (hideWhenNotInteractable)
            {
                Debug.Log("Hidable button visibility: " + value);
                gameObject.SetActive(value);
            }
            button.interactable = value;
            glowImage.enabled = value;
            if (!_isAnimating && value)  StartCoroutine(AnimateGlow());
        }

        private IEnumerator AnimateGlow()
        {
            _isAnimating = true;

            var targetScale = FullPulseSize;

            var direction = targetScale.x > glowImage.transform.localScale.x;
        
            while (_isAnimating)
            {
                if (!isInteractable)
                {
                    _isAnimating = false;
                    yield return null;
                }
            
                var increment = animateSpeed * Time.deltaTime * new Vector3(1f, 1f, 0f);
            
                switch (direction)
                {
                    case true:
                        glowImage.transform.localScale += increment;
                        if (glowImage.transform.localScale.x > targetScale.x)
                        {
                            direction = false;
                            targetScale = MinGlowSize;
                        }
                        break;
                    case false:
                        glowImage.transform.localScale -= increment;
                        if (glowImage.transform.localScale.x < targetScale.x)
                        {
                            direction = true;
                            targetScale = FullPulseSize;
                        }
                        break;
                }

                yield return null;
            }
            glowImage.transform.localScale = MinGlowSize;
        }
    }
}
