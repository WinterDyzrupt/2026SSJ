using System.Collections.Generic;
using System.Linq;
using Common.Data.Fragments;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class ComputationArea : MonoBehaviour
    {
        [SerializeField] private FragmentMapper mapper;
        [SerializeField] private List<FragmentDropSlot> slots;
        [SerializeField] private FragmentStorageArea storageArea;

        private void Awake()
        {
            Debug.Assert(mapper != null, nameof(mapper) + " != null");
            Debug.Assert(slots != null, nameof(slots) + " != null");
            Debug.Assert(storageArea != null, $"{nameof(storageArea)} != null");

            foreach (var slot in slots)
            {
                slot.OccupancyChanged += CheckFragmentsAgainstMap;
            }
        }

        private void OnDestroy()
        {            
            foreach (var slot in slots)
            {
                slot.OccupancyChanged -= CheckFragmentsAgainstMap;
            }
        }

        private void CheckFragmentsAgainstMap()
        {
            var allFragmentData = slots
                .Where(x => x.OccupiedFragment != null)
                .Select(x => x.OccupiedFragment.Data)
                .ToList();

            if (mapper.FindFragment(allFragmentData, out var results))
            {
                foreach (var slot in slots)
                {
                    storageArea.UsedFragments.Add(slot.OccupiedFragment.Data);
                    slot.OccupiedFragment.DestroyFragment();
                    slot.UnregisterFragment();
                }
                
                storageArea.AddNewFragment(results);
            }
        }
    }
}