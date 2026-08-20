using UnityEngine;
using Common.Data.Dialog.Lines;

namespace Common.Data.Dialog
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Script")]
    public class Script : ScriptableObject
    {
        public ScriptChunk intro;
        public ScriptChunk otherText;
        public ScriptChunk tutorial;
    }
}