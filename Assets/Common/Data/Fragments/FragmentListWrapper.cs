using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentListWrapper", menuName = "Fragments/List Wrapper")]
    public class FragmentListWrapper : ScriptableObject
    {
        [SerializeField] private List<FragmentData> currentList;

        public event Action<List<FragmentData>> NewListProvided;

        private void Awake()
        {
            currentList = new();
        }

        public void NewList(List<FragmentData> newList)
        {
            if (currentList == newList) return;
            currentList = newList;
            NewListProvided?.Invoke(currentList);
        }
        
        public static implicit operator List<FragmentData>(FragmentListWrapper wrapper) => wrapper.currentList;
    }
}