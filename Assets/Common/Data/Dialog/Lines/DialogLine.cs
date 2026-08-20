using System.Collections.Generic;
using UnityEngine;
using Common.Data.Dialog.Participants;

namespace Common.Data.Dialog.Lines
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Line")]
    public class Line : ScriptableObject
    {
        public string text;
        
        /// <summary>
        /// Null if no speaker (e.g. narrator/tutorial).
        /// </summary>
        public DialogParticipant speaker;

        /// <summary>
        /// Empty if there are no listeners (e.g. talking to self or just thinking).
        /// </summary>
        public List<DialogParticipant> listeners = new();
    }
}