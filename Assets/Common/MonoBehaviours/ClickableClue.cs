using System.Collections;
using Common.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours
{
    public class ClickableClue : MonoBehaviour
    {
        public Image buttonImage;
        public Image glowImage;
        public Button button;

        public bool canBeInteractedWithMultipleTimes;

        /// <summary>
        /// Whether this specific clue is interactable.  This is set to false when this clue is clicked, preventing
        /// this clue from being clicked again.
        /// </summary>
        public BoolWrapper isInteractable;
        
        /// <summary>
        /// Whether any/all clues are interactable.  This is set to false when dialog or the mind palace are visible.
        /// This prevents clues from glowing/etc. when something else is happening.
        /// </summary>
        public BoolWrapper cluesAreInteractable;

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
            Debug.Assert(cluesAreInteractable != null, $"{nameof(cluesAreInteractable)} wasn't assigned.");

            glowImage.sprite = buttonImage.sprite;
            glowImage.color = glowColor;

            cluesAreInteractable.Changed += SetInteractable;
            isInteractable.Changed += SetInteractable;

            SetInteractable();
        }

        private void OnDestroy()
        {
            cluesAreInteractable.Changed -= SetInteractable;
            isInteractable.Changed -= SetInteractable;
        }

        public void OnClueClicked()
        {
            if (!canBeInteractedWithMultipleTimes)
            {
                isInteractable.Set(false);
            }
        }

        /// <summary>
        /// Sets this clue to be interactable based on the clue-specific flag and the all-clues flag.
        /// </summary>
        private void SetInteractable()
        {
            var isThisClueInteractable = isInteractable && cluesAreInteractable; 

            SetInteractable(isThisClueInteractable);
        }

        private void SetInteractable(bool value)
        {
            button.interactable = value;
            glowImage.enabled = value;
            if (!_isAnimating && value)  StartCoroutine(AnimateClue());
        }

        private IEnumerator AnimateClue()
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
