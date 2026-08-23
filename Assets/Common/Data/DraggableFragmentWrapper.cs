using System;
using Common.MonoBehaviours.MindPalace;
using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "DraggableFragmentWrapper", menuName = "Data/Draggable Fragment Wrapper")]
    public class DraggableFragmentWrapper : ScriptableObject
    {
        public DraggableFragment CurrentFragment { get; private set; }

        public event Action Changed;

        private void Awake() => CurrentFragment = null;
        
        public void Set(DraggableFragment newFragment)
        {
            if (CurrentFragment == newFragment) return;
            CurrentFragment = newFragment;
            Changed?.Invoke();
        }
    }
}