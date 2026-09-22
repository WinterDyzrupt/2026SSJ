using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Data.Fragments;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class FragmentStorageArea : MonoBehaviour
    {
        [SerializeField] private List<FragmentDropSlot> slots;
        [SerializeField] private GameObject fragmentPrefab;
        [SerializeField] private Transform fragmentParent;
        [SerializeField] private FragmentDataListWrapper usedFragments;

        [Header("Wrappers")]
        [SerializeField] private FragmentDataListWrapper newFragmentsToAdd;
        
        [SerializeField] private List<FragmentData> initialFragments;

        /// <summary>
        /// Flag indicating whether this object's Start() has been called; this is important because if it has been
        /// called, other objects' Awakes have been called (slots), meaning they can be used.  We want logic to happen
        /// OnEnable, but during the initial OnEnable, the slots have not had Awake called.
        /// </summary>
        private bool _isStarted;
        
        private void Awake()
        {
            Debug.Assert(slots.Count != 0,"No slot assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentPrefab != null, "No fragment prefab assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentParent != null, "No fragment parent assigned to the Fragment Storage Area.");
            Debug.Assert(newFragmentsToAdd != null, "No fragment to be added to the Fragment Storage Area.");
            
            newFragmentsToAdd.NewFragmentAdded += AddNewFragment;
            // If we don't force an update, Canvas object don't have proper rect transform positions
            // this would cause any grabbed transform data to be wrong
            Canvas.ForceUpdateCanvases();
        }

        private void OnDestroy()
        {
            newFragmentsToAdd.NewFragmentAdded += AddNewFragment;
        }

        /// <summary>
        /// Note: This object will be Awake and Enabled before any of its children objects (slots) are Awake.
        /// It is not safe to use children objects in OnEnable.
        /// </summary>
        private void OnEnable()
        {
            Debug.Log("Enabled storage area.");
            if (_isStarted)
            {
                AddPreviousNewFragments();
            }
            else
            {
                Debug.Log("Skipping adding new fragments for first OnEnable.");
            }
        }

        private void OnDisable()
        {
            Debug.Log("Disabled storage area.");
        }

        private void Start()
        {
            Debug.Log("Starting storage area; now it's safe to add new fragments.");
            _isStarted = true;
            AddPreviousNewFragments();
        }

        /// <summary>
        /// Add fragments that were added before this object was active.
        /// Interacts with other game objects; make sure this is called after Awake().
        /// </summary>
        private void AddPreviousNewFragments()
        {
            ForceSlotUpdate();

            if (newFragmentsToAdd.isElementPresent)
            {
                Debug.Log("New fragment present.");
                foreach (var newFragmentData in newFragmentsToAdd.GetElements())
                {
                    AddNewFragmentCore(newFragmentData);
                }

                newFragmentsToAdd.Clear();
            }
        }

        private void AddNewFragment(FragmentData newFragmentData)
        {
            Debug.Assert(newFragmentData != null, nameof(newFragmentData) + " expected to be non-null.");
            
            if (gameObject.activeInHierarchy)
            {
                AddNewFragmentCore(newFragmentData);
                newFragmentsToAdd.Clear();
            }
            else
            {
                Debug.Log("New fragment added while mind palace is not active; will add when mind palace is enabled.");
            }
        }
        
        private void AddNewFragmentCore(FragmentData newFragmentData)
        {
            Debug.Log("Adding new fragment: " + newFragmentData);
            var availableSlot = slots.FirstOrDefault(x => !x.IsOccupied);
            if (!availableSlot)
            {
                Debug.LogError("No available slot found for new fragment!");
                return;
            }
            
            var newFragmentObject = Instantiate(fragmentPrefab, fragmentParent);
            var newFragment = newFragmentObject.GetComponent<DraggableFragment>();
            newFragment.InitializeFragment(newFragmentData);
            availableSlot.RegisterFragment(newFragment);
        }

        private void ForceSlotUpdate()
        {
            List<FragmentData> usedList = usedFragments;
            foreach (var slot in slots)
            {
                if(usedList.Contains(slot.OccupiedFragment?.Data)) slot.UnregisterFragment();
            }
        }
    }
}