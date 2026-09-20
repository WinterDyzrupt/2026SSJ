using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentDataListWrapper", menuName = "Fragments/List Wrapper")]
    public class FragmentDataListWrapper : ScriptableObject
    {
        [SerializeField] protected List<FragmentData> currentList;

        public event Action<FragmentData> NewFragmentAdded;

        private void OnEnable()
        {
            currentList = new();
        }

        public void Add(FragmentData fragment)
        {
            Debug.Log("Adding new fragment: " + fragment);
            currentList.Add(fragment);
            NewFragmentAdded?.Invoke(fragment);
        }

        public void Add(List<FragmentData> fragments)
        {
            foreach (var fragment in fragments)
            {
                Add(fragment);
            }
        }

        public static implicit operator List<FragmentData>(FragmentDataListWrapper wrapper) => wrapper.currentList;
    }
}