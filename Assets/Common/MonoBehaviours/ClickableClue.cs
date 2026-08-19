using System.Collections;
using Common.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours
{
    public class ClickableClue : MonoBehaviour
    {
        [Header("When setting sprite, only set on button child image component.")]
        [SerializeField] private Image buttonImage;
        [SerializeField] private Image glowImage;
        [SerializeField] private Button button;
        [SerializeField] private BoolWrapper isInteractable;
        // [SerializeField] private ??? Something to trigger when clicked;

        [Header("Glow Variables")]
        public Color glowColor;
        [Range(1, 2)] public float glowSize = 1.1f;
        [Range(0, 1)] public float pulseSize = 0.2f;
        [Range(0,1)] public float animateSpeed = 0.4f;

        private bool _isAnimating = false;
        private Vector3 MinGlowSize => new Vector3(glowSize, glowSize, 0);
        private Vector3 FullPulseSize => new Vector3(glowSize + pulseSize, glowSize + pulseSize, 0);
    
        private void Awake()
        {
            Debug.Assert(buttonImage != null, "Button Image wasn't assigned.");
            Debug.Assert(glowImage != null, "Glow Image wasn't assigned.");
            Debug.Assert(button != null, "Button wasn't assigned.");
            Debug.Assert(isInteractable, "Interactable wasn't assigned.");
        
            glowImage.sprite = buttonImage.sprite;
            glowImage.color = glowColor;
        
            isInteractable.Changed += SetInteractable;
            
            SetInteractable();
        }

        private void OnDestroy()
        {
            isInteractable.Changed -= SetInteractable;
        }

        public void WhenClueClicked()
        {
            isInteractable.Changed -= SetInteractable;
            SetInteractable(false);
            // TODO: Setup to do something
        }

        private void SetInteractable() => SetInteractable(isInteractable);

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
