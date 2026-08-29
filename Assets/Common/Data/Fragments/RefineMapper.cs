using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "RefineMapper", menuName = "Fragments/Refine Mapper")]
    public class RefineMapper : FragmentMapper
    {
        public override List<FragmentData> FindFragment(List<FragmentData> targets)
        {
            if (targets.Count != 1)
            {
                Debug.LogError($"Refine Mapper was given {targets.Count} targets. Expected 1.");
                return null;
            }

            if (map.Any(x => x.from.Count != 1) || map.Any(x => x.to.Count != 1) )
            {
                Debug.LogError($"Refine Mapper entries must have exactly 1 'from' and 1 'to'.");
                return null;
            }
            
            var target = targets[0];

            return map.FirstOrDefault(x => x.from[0] == target).to;
        }
    }
}