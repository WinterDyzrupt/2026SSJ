//using Common.Data;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class MindPalaceController : MonoBehaviour
    {
        //[Header("Wrappers")]
        //[SerializeField] private BoolWrapper isMindPalaceActive;
        
        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private FragmentStorageArea storageArea;

        private void Awake()
        {
            //Debug.Assert(isMindPalaceActive != null, nameof(isMindPalaceActive) + " != null");
            Debug.Assert(canvasGroup != null, nameof(canvasGroup) + " != null");
            Debug.Assert(storageArea != null, nameof(storageArea) + " != null");
            
            //isMindPalaceActive.Changed += UpdateVisibility;

            //UpdateVisibility();
        }

        // private void OnDestroy()
        // {
        //     //isMindPalaceActive.Changed -= UpdateVisibility;
        // }

        // private void UpdateVisibility()
        // {
        //     //Debug.LogError("Mind palace visibility: " + isMindPalaceActive);
        //     // canvasGroup.interactable = isMindPalaceActive;
        //     // canvasGroup.blocksRaycasts = isMindPalaceActive;
        //     // canvasGroup.alpha = isMindPalaceActive ? 1 : 0;
        //     
        //     //gameObject.SetActive(isMindPalaceActive.currentValue);
        // }

        public void OpenMindPalace()
        {
            SetVisibility(true);
        }

        public void CloseMindPalace()
        {
            SetVisibility(false);
        }

        private void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}