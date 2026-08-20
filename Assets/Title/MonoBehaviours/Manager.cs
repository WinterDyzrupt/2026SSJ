using Common.Utilities;
using Common.Data.Dialog;
using UnityEngine;

namespace Title.MonoBehaviours
{
    public class Manager : MonoBehaviour
    {
        public Script script;
        public ScriptChunk introChunk;
        public void Start()
        {
            DialogHelper.StartDialog(script.intro);
        }
    }
}
