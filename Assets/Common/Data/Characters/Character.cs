using UnityEngine;

namespace Common.Data.Characters
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Characters/Character")]
    public class Character : ScriptableObject
    {
        public string displayName;
        // portrait/bust/model/etc. to display
    }
}