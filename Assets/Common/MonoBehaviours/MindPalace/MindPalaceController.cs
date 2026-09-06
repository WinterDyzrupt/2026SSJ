using System.Collections.Generic;
using Common.Data;
using Common.Data.Fragments;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class MindPalaceController : MonoBehaviour
    {
        [Header("Wrappers")]
        [SerializeField] private FragmentDataListWrapper newFragmentsToAdd;
        [SerializeField] private BoolWrapper isMindPalaceActive;
        
        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private FragmentStorageArea storageArea;

        [Header("Initial fragments to spawn for testing")]
        [SerializeField] private List<FragmentData> initialFragments;

        private List<FragmentData> _queuedFragments;

        private void Awake()
        {
            Debug.Assert(newFragmentsToAdd != null, nameof(newFragmentsToAdd) + " != null");
            Debug.Assert(isMindPalaceActive != null, nameof(isMindPalaceActive) + " != null");
            Debug.Assert(canvasGroup != null, nameof(canvasGroup) + " != null");
            Debug.Assert(storageArea != null, nameof(storageArea) + " != null");

            _queuedFragments = new();

            newFragmentsToAdd.NewListProvided += AddFragmentsToQueue;
            newFragmentsToAdd.NewFragmentAdded += AddFragmentToQueue;
            isMindPalaceActive.Changed += ToggleMindPalace;

            ToggleMindPalace();
        }

        private void OnDestroy()
        {
            newFragmentsToAdd.NewListProvided -= AddFragmentsToQueue;
            newFragmentsToAdd.NewFragmentAdded -= AddFragmentToQueue;
            isMindPalaceActive.Changed -= ToggleMindPalace;
        }

        private void Update()
        {
            /*
            // for testing. Have dialogue control initial fragments instead.
            if (initialFragments.Count > 0)
            {
                newFragmentsToAdd.SetList(initialFragments);
                initialFragments.Clear();
            }
            */
            
            CheckToGenerateFragments();
        }

        private void AddFragmentsToQueue(List<FragmentData> newFragments)
        {
            _queuedFragments.AddRange(newFragments);
            
            CheckToGenerateFragments();
        }

        private void AddFragmentToQueue(FragmentData newFragment)
        {
            _queuedFragments.Add(newFragment);
            
            CheckToGenerateFragments();
        }

        private void ToggleMindPalace()
        {
            canvasGroup.interactable = isMindPalaceActive;
            canvasGroup.blocksRaycasts = isMindPalaceActive;
            canvasGroup.alpha = isMindPalaceActive ? 1 : 0;
            
            CheckToGenerateFragments();
        }

        private void CheckToGenerateFragments()
        {
            if (!isMindPalaceActive || _queuedFragments.Count < 1) return;
            
            storageArea.AddNewFragment(_queuedFragments);
            _queuedFragments.Clear();
        }
    }
}