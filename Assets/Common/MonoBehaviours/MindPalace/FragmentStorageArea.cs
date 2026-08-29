using System.Collections.Generic;
using System.Linq;
using Common.Data.Fragments;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class FragmentStorageArea : MonoBehaviour
    {
        [SerializeField] private List<FragmentDropSlot> slots;
        [SerializeField] private GameObject fragmentPrefab;
        [SerializeField] private Transform fragmentParent;

        [Header("Initial fragments to spawn for testing")]
        [SerializeField] private List<FragmentData> initialFragments;

        private void Awake()
        {
            Debug.Assert(slots.Count != 0,"No slot assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentPrefab != null, "No fragment prefab assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentParent != null, "No fragment parent assigned to the Fragment Storage Area.");
            
            Canvas.ForceUpdateCanvases();
        }

        private void Start()
        {
            AddNewFragment(initialFragments);
        }

        private void AddNewFragment(FragmentData newFragmentData)
        {
            var availableSlot = slots.FirstOrDefault(x => !x.IsOccupied);
            if (availableSlot == null)
            {
                Debug.LogError("No available slot found for new fragment!");
                return;
            }
            
            var newFragmentObject = Instantiate(fragmentPrefab, fragmentParent);
            var newFragment = newFragmentObject.GetComponent<DraggableFragment>();
            newFragment.InitializeFragment(newFragmentData,availableSlot);
        }

        public void AddNewFragment(List<FragmentData> newFragmentsData)
        {
            foreach (var newFragment in newFragmentsData) AddNewFragment(newFragment);
        }
    }
}