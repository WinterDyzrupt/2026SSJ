using System;
using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "MonoBehaviourWrapper", menuName = "Data/MonoBehaviour Wrapper")]
    public class MonoBehaviourWrapper : ScriptableObject
    {
        public MonoBehaviour Current { get; private set; }

        public event Action ReferenceChanged;
        
        private void Awake() => Current = null;
        
        public void Set(MonoBehaviour newMonoBehaviour)
        {
            if (Current == newMonoBehaviour) return;
            Current = newMonoBehaviour;
            ReferenceChanged?.Invoke();
        }
    }
}