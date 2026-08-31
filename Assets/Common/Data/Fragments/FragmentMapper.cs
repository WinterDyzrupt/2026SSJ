using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    public abstract class FragmentMapper : ScriptableObject
    {
        /// <summary>
        /// Compares the list of fragment data to the inherited map. Returns true if any matches were found.
        /// </summary>
        /// <param name="candidates">The list of fragment data to be mapped.</param>
        /// <param name="results">The list of fragment data returns on successful map. Will be empty if none found.</param>
        /// <returns></returns>
        public abstract bool FindFragment(List<FragmentData> candidates, out List<FragmentData> results);
    }
}