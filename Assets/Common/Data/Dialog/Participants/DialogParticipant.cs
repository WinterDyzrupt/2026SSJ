using UnityEngine;
using Common.Data.Characters;

namespace Common.Data.Dialog.Participants
{
    /// <summary>
    /// Currently a placeholder wrapper class, in case we want to show non-speaker characters during a dialog. 
    /// </summary>
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Participant")]
    public class DialogParticipant : ScriptableObject
    {
        public Character character;

        public ParticipantSide side;

        public override string ToString()
        {
            return $"Side: {side}; character: {character}";
        }
    }
}