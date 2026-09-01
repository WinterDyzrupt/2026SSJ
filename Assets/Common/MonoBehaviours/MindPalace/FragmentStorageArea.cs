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
        
        public readonly List<FragmentData> UsedFragments = new();

        private void Awake()
        {
            Debug.Assert(slots.Count != 0,"No slot assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentPrefab != null, "No fragment prefab assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentParent != null, "No fragment parent assigned to the Fragment Storage Area.");
            
            // If we don't force an update, Canvas object don't have proper rect transform positions
            // this would cause any grabbed transform data to be wrong
            Canvas.ForceUpdateCanvases();
        }

        private void Start()
        {
            AddNewFragment(initialFragments);
        }
        
        // TODO: Wire this up to an event that exists in a scriptable object
        public void AddNewFragment(List<FragmentData> newFragmentsData)
        {
            ForceSlotUpdate();
            
            foreach (var newFragmentData in newFragmentsData)
            {
                var availableSlot = slots.FirstOrDefault(x => !x.IsOccupied);
                if (availableSlot == null)
                {
                    Debug.LogError("No available slot found for new fragment!");
                    return;
                }
            
                var newFragmentObject = Instantiate(fragmentPrefab, fragmentParent);
                var newFragment = newFragmentObject.GetComponent<DraggableFragment>();
                newFragment.InitializeFragment(newFragmentData);
                availableSlot.RegisterFragment(newFragment);
            }
        }

        private void ForceSlotUpdate()
        {
            foreach (var slot in slots)
            {
                if(UsedFragments.Contains(slot.OccupiedFragment?.Data)) slot.UnregisterFragment();
            }
        }
    }
}