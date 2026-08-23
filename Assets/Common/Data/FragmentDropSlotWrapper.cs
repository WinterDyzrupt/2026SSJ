using System;
using Common.MonoBehaviours.MindPalace;
using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "FragmentDropSlotWrapper", menuName = "Data/Fragment Drop Slot Wrapper")]
    public class FragmentDropSlotWrapper : ScriptableObject
    {
        public FragmentDropSlot CurrentDropSlot { get; private set; }

        public event Action Changed;

        private void Awake() => CurrentDropSlot = null;
        
        public void Set(FragmentDropSlot newSlot)
        {
            if (CurrentDropSlot == newSlot) return;
            CurrentDropSlot = newSlot;
            Changed?.Invoke();
        }
    }
}