using System;
using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "BoolWrapper", menuName = "Data/Bool Wrapper")]
    public class BoolWrapper : ScriptableObject
    {
        public bool currentValue;
        public bool defaultValue;

        public event Action Changed;

        private void OnEnable()
        {
            currentValue = defaultValue;
        }

        public void Set(bool newBool)
        {
            if (currentValue == newBool) return;
            currentValue = newBool;
            Changed?.Invoke();
        }

        [ContextMenu("Toggle")]
        public void Toggle()
        {
            currentValue = !currentValue;
            Changed?.Invoke();
        }

        public static implicit operator bool(BoolWrapper value) => value.currentValue;
    }
}