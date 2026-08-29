using UnityEngine;

namespace Common.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentData", menuName = "Fragments/Fragment Data")]
    public class FragmentData : ScriptableObject
    {
        public string subject;
        public Color color;
        [TextArea(20,50)] public string description;
        // What else does this need?
    }
}