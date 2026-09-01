using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "SplitMapper", menuName = "Fragments/Split Mapper")]
    public class SplitMapper : FragmentMapper
    {
        [System.Serializable]
        public struct SplitEntry
        {
            [SerializeField] public FragmentData input1;
            [SerializeField] public FragmentData output1;
            [SerializeField] public FragmentData output2;
            
            public bool IsValid => input1 != null && output1 != null && output2 != null;
        }
        
        [SerializeField] public List<SplitEntry> splitMap;

        private void Awake()
        {
            if (!splitMap.All(x => x.IsValid))
            {
                Debug.LogError($"Split Mapper in {name} has a null value in one its entries");
            }
        }
        
        public override bool FindFragment(List<FragmentData> candidates, out List<FragmentData> results)
        {
            results = new();
            
            if (candidates?.Count != 1 || candidates[0] == null)
            {
                return false;
            }
            
            var match = splitMap.FirstOrDefault(x => x.input1 == candidates[0]);

            if (!match.IsValid) return false;
            
            results.Add(match.output1);
            results.Add(match.output2);
            return true;
        }
    }
}