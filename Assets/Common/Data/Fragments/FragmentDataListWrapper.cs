using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentDataListWrapper", menuName = "Fragments/List Wrapper")]
    public class FragmentDataListWrapper : ScriptableObject
    {
        [SerializeField] protected List<FragmentData> currentList;
        public BoolWrapper isElementPresent;

        public event Action<FragmentData> NewFragmentAdded;

        public void Awake()
        {
            Debug.AssertFormat(isElementPresent != null, nameof(isElementPresent) + " expected to be non-null.");
        }

        private void OnEnable()
        {
            currentList = new();
        }

        public void Add(FragmentData fragment)
        {
            Debug.Assert(fragment != null, nameof(fragment) + " expected to be non-null.");
            
            Debug.Log("Adding new fragment: " + fragment);
            currentList.Add(fragment);
            NewFragmentAdded?.Invoke(fragment);
            isElementPresent.Set(true);
        }

        public void Add(List<FragmentData> fragments)
        {
            foreach (var fragment in fragments)
            {
                Add(fragment);
            }
        }

        public List<FragmentData> GetElements()
        {
            return currentList;
        }

        public void Clear()
        {
            currentList.Clear();
            isElementPresent.Set(false);
        }

        public static implicit operator List<FragmentData>(FragmentDataListWrapper wrapper) => wrapper.currentList;
    }
}