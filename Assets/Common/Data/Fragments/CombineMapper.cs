using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "CombineMapper", menuName = "Fragments/Combine Mapper")]
    public class CombineMapper : FragmentMapper
    {
        public override List<FragmentData> FindFragment(List<FragmentData> targets)
        {
            if (targets.Count == 1) return null; // User hasn't selected a second fragment yet
            if (targets.Count != 2)
            {
                Debug.LogError($"Combine Mapper was given {targets.Count} targets. 1 is fine but 2 are needed.");
                return null;
            }

            if (map.Any(x => x.from.Count != 2) || map.Any(x => x.to.Count != 1) )
            {
                Debug.LogError($"Combine Mapper entries must have exactly 2 'from' and 1 'to'.");
                return null;
            }
            
            return map.FirstOrDefault(x =>
                (x.from[0] == targets[0] && x.from[1] == targets[1]) ||
                (x.from[0] == targets[1] && x.from[1] == targets[0]))
                .to;
        }
    }
}