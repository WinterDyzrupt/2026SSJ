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
        [SerializeField] private FragmentDataListWrapper createFragmentWrapper;
        [SerializeField] private FragmentDataListWrapper usedFragmentsWrapper;
        

        private void Awake()
        {
            Debug.Assert(mapper != null, nameof(mapper) + " != null");
            Debug.Assert(slots != null, nameof(slots) + " != null");
            Debug.Assert(createFragmentWrapper != null, $"{nameof(createFragmentWrapper)} != null");

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
                .Where(x => x.OccupiedFragment)
                .Select(x => x.OccupiedFragment.Data)
                .ToList();

            if (mapper.FindFragment(allFragmentData, out var results))
            {
                foreach (var slot in slots)
                {
                    usedFragmentsWrapper.Add(slot.OccupiedFragment.Data);
                    slot.OccupiedFragment.DestroyFragment();
                    slot.UnregisterFragment();
                }
                
                createFragmentWrapper.SetList(results);
            }
        }
    }
}