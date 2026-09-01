using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "CombineMapper", menuName = "Fragments/Combine Mapper")]
    public class CombineMapper : FragmentMapper
    {
        [System.Serializable]
        public struct CombineEntry
        {
            [SerializeField] public FragmentData input1;
            [SerializeField] public FragmentData input2;
            [SerializeField] public FragmentData output1;
            
            public bool IsValid => input1 != null && input2 != null && output1 == null;
        }
        
        [SerializeField] public List<CombineEntry> combineMap;

        private void Awake()
        {
            if (!combineMap.All(x => x.IsValid))
            {
                Debug.LogError($"Combine Mapper in {name} has a null value in one its entries.");
            }
        }

        public override bool FindFragment(List<FragmentData> candidates, out List<FragmentData> results)
        {
            results = new();

            if (candidates?.Count != 2 || candidates[0] == null || candidates[1] == null)
            {
                return false;
            }
            
            var match =  combineMap.FirstOrDefault(x =>
                (x.input1 == candidates[0] && x.input2 == candidates[1]) ||
                (x.input1 == candidates[1] && x.input2 == candidates[0]))
                .output1;

            if (match == null) return false;
                
            results.Add(match);
            return true;
        }
    }
}