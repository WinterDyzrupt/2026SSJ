using Common.Data;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class MindPalaceController : MonoBehaviour
    {
        [Header("Wrappers")]
        [SerializeField] private BoolWrapper isMindPalaceActive;
        
        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private FragmentStorageArea storageArea;

        private void Awake()
        {
            Debug.Assert(isMindPalaceActive != null, nameof(isMindPalaceActive) + " != null");
            Debug.Assert(canvasGroup != null, nameof(canvasGroup) + " != null");
            Debug.Assert(storageArea != null, nameof(storageArea) + " != null");
            
            isMindPalaceActive.Changed += UpdateVisibility;

            UpdateVisibility();
        }

        private void OnDestroy()
        {
            isMindPalaceActive.Changed -= UpdateVisibility;
        }
        

        private void UpdateVisibility()
        {
            canvasGroup.interactable = isMindPalaceActive;
            canvasGroup.blocksRaycasts = isMindPalaceActive;
            canvasGroup.alpha = isMindPalaceActive ? 1 : 0;
        }
    }
}