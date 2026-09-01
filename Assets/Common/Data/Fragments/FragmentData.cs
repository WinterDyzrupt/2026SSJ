using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentData", menuName = "Fragments/Fragment Data")]
    public class FragmentData : ScriptableObject
    {
        public string displayName;
        public Color color;
        [TextArea(20,50)] public string description;
        // What else does this need?

        public override string ToString()
        {
            return $"Name: {name} - DisplayName: {displayName}.";
        }
    }
}