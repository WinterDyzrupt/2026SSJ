using System.Collections.Generic;
using UnityEngine;
using Common.Data.Dialog.Lines;

namespace Common.Data.Dialog
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/ScriptChunk")]
    public class ScriptChunk : ScriptableObject
    {
        public List<Line> lines;
    }
}