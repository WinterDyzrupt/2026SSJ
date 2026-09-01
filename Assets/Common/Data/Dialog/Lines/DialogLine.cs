using UnityEngine;
using Common.Data.Dialog.Participants;

namespace Common.Data.Dialog.Lines
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Line")]
    public class Line : ScriptableObject
    {
        /// <summary>
        /// Null if no speaker (e.g. narrator/tutorial).
        /// </summary>
        public DialogParticipant leftParticipant;

        /// <summary>
        /// Null if no listener (e.g. thinking to self).
        /// </summary>
        public DialogParticipant rightParticipant;

        public ParticipantSide sideSpeakerIsOn;

        [TextArea(5,5)] public string text;

        public override string ToString()
        {
            DialogParticipant speaker;
            DialogParticipant listener;
            
            switch (sideSpeakerIsOn)
            {
                case ParticipantSide.Left:
                    speaker = leftParticipant;
                    listener = rightParticipant;
                    break;
                case ParticipantSide.Right:
                    speaker = rightParticipant;
                    listener = leftParticipant;
                    break;
                case ParticipantSide.None:
                default:
                    return "No speaker; Text: " + text;
            }

           return $": {speaker}; Text: {text}; listener: {listener}";
        }
    }
}