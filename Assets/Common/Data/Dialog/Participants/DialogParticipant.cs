using UnityEngine;
using Common.Data.Characters;

namespace Common.Data.Dialog.Participants
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Participant")]
    public class DialogParticipant : ScriptableObject
    {
        public Character character;
        public DialogSide dialogSide;
    }
}