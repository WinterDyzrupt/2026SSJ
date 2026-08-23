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
        public DialogParticipant speaker;

        public string text;

        public override string ToString()
        {
           return $"Speaker: {speaker}; Text: {text}";
        }
    }
}