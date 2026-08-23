using UnityEngine;

namespace Common.Data.Characters
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Characters/Character")]
    public class Character : ScriptableObject
    {
        public string displayName;

        public Color color;
        
        // portrait/bust/model/etc. to display

        public override string ToString()
        {
            return displayName;
        }
    }
}