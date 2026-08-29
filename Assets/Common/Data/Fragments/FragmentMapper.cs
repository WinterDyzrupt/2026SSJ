using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    public abstract class FragmentMapper : ScriptableObject
    {
        [System.Serializable]
        public struct FragmentMap
        {
            [SerializeField] public List<FragmentData> from;
            [SerializeField] public List<FragmentData> to;
        }
        
        [SerializeField] public List<FragmentMap> map;

        public virtual List<FragmentData> FindFragment(List<FragmentData> target) { return null; }
    }
}