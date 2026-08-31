using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "RefineMapper", menuName = "Fragments/Refine Mapper")]
    public class RefineMapper : FragmentMapper
    {
        [System.Serializable]
        public struct RefineEntry
        {
            [SerializeField] public FragmentData input1;
            [SerializeField] public FragmentData output1;
            
            public bool IsNotValid => input1 == null || output1 == null;
        }
        
        [SerializeField] public List<RefineEntry> refineMap;

        private void Awake()
        {
            if (refineMap.Any(x => x.IsNotValid))
            {
                Debug.LogError($"Refine Mapper in {name} has a null value in one it's entries.");
            }
        }

        public override bool FindFragment(List<FragmentData> candidates, out List<FragmentData> results)
        {
            results = new();
            
            if (candidates?.Count != 1 || candidates[0] == null)
            {
                return false;
            }
            
            var match = refineMap.FirstOrDefault(x =>
                x.input1 == candidates[0])
                .output1;

            if (match == null) return false;

            results.Add(match);
            return true;
        }
    }
}