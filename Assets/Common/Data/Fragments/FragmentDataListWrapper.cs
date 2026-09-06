using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentDataListWrapper", menuName = "Fragments/List Wrapper")]
    public class FragmentDataListWrapper : ScriptableObject
    {
        [SerializeField] private List<FragmentData> currentList;

        public event Action<List<FragmentData>> NewListProvided;
        public event Action<FragmentData> NewFragmentAdded;

        private void OnEnable()
        {
            currentList = new();
        }

        public void SetList(List<FragmentData> newList)
        {
            if (currentList == newList) return;
            currentList = newList;
            NewListProvided?.Invoke(currentList);
        }
        
        public void Add(FragmentData fragment)
        {
            currentList.Add(fragment);
            NewFragmentAdded?.Invoke(fragment);
        }
        
        public static implicit operator List<FragmentData>(FragmentDataListWrapper wrapper) => wrapper.currentList;
    }
}