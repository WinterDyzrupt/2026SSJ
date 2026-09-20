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
        //[SerializeField] private BoolWrapper isMindPalaceActive;
        //[SerializeField] private FragmentDataListWrapper newFragmentsToAdd;
        public NewClueQueue newClueQueue;

        [SerializeField] private List<FragmentData> initialFragments;
        //private readonly List<FragmentData> _queuedFragments = new();
        
        private void Awake()
        {
            Debug.Assert(slots.Count != 0,"No slot assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentPrefab != null, "No fragment prefab assigned to the Fragment Storage Area.");
            Debug.Assert(fragmentParent != null, "No fragment parent assigned to the Fragment Storage Area.");
            //Debug.Assert(isMindPalaceActive != null, "No fragment parent active assigned to the Fragment Storage Area.");
            //Debug.Assert(newFragmentsToAdd != null, "No fragment to be added to the Fragment Storage Area.");
            Debug.Assert(newClueQueue != null, nameof(newClueQueue) + " expected to be non-null.");
            
            
            //newFragmentsToAdd.NewFragmentAdded += AddFragmentToQueue;
                
            // If we don't force an update, Canvas object don't have proper rect transform positions
            // this would cause any grabbed transform data to be wrong
            Canvas.ForceUpdateCanvases();
        }

        private void OnDestroy()
        {
            //newFragmentsToAdd.NewFragmentAdded -= AddFragmentToQueue;
        }

        private void Update()
        {
            /*// for testing. Have dialogue control initial fragments instead.
            if (initialFragments.Count > 0)
            {
                newFragmentsToAdd.Add(initialFragments);
                initialFragments.Clear();
            }*/
            
            CheckToGenerateFragments();
        }

        private void AddNewFragment(List<FragmentData> newFragmentsData)
        {
            ForceSlotUpdate();
            
            foreach (var newFragmentData in newFragmentsData)
            {
                Debug.LogError("Adding new fragment: " + newFragmentData);
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
        }

        private void ForceSlotUpdate()
        {
            List<FragmentData> usedList = usedFragments;
            foreach (var slot in slots)
            {
                if(usedList.Contains(slot.OccupiedFragment?.Data)) slot.UnregisterFragment();
            }
        }

        // private void AddFragmentToQueue(FragmentData newFragment)
        // {
        //     _queuedFragments.Add(newFragment);
        //     
        //     CheckToGenerateFragments();
        // }
        
        private void CheckToGenerateFragments()
        {
            //if (!isMindPalaceActive || _queuedFragments.Count < 1) return;
            //if (_queuedFragments.Count < 1)
            //{
            //     return;
            // }
            //
            // AddNewFragment(_queuedFragments);
            // _queuedFragments.Clear();
            if (newClueQueue.newCluePresent)
            {
                Debug.LogError("New clue present.");
                AddNewFragment(newClueQueue.GetNewClues());
                newClueQueue.Clear();
            }
        }
    }
}