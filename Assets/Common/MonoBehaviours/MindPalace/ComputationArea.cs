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
                .Select(x => x.OccupiedFragment.FragmentData)
                .ToList();
            var results = mapper.FindFragment(allFragmentData);

            if (results?.Count > 0)
            {
                var allFragments = slots
                    .Where(x => x.OccupiedFragment != null)
                    .Select(x => x.OccupiedFragment)
                    .ToList();
                foreach (var fragment in allFragments) fragment.DestroyFragment();
                
                storageArea.AddNewFragment(results);
            }
        }
    }
}