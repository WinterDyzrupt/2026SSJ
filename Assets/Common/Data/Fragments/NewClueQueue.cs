using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "NewClueQueue", menuName = "Fragments/New Clue Queue")]
    public class NewClueQueue : FragmentDataListWrapper
    {
        public BoolWrapper newCluePresent;

        public void Awake()
        {
            Debug.AssertFormat(newCluePresent != null, nameof(newCluePresent) + " expected to be non-null.");
        }

        public new void Add(FragmentData clue)
        {
            Debug.Assert(clue != null, nameof(clue) + " expected to be non-null.");

            Debug.Log("Adding new clue " + clue);
            base.Add(clue);
            newCluePresent.Set(true);
        }

        public List<FragmentData> GetNewClues()
        {
            return currentList;
        }

        public void Clear()
        {
            currentList.Clear();
            newCluePresent.Set(false);
        }
    }
}