using Common.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours.MindPalace
{
    public class FragmentDropSlot : MonoBehaviour
    {
        [Header("Wrappers")]
        [SerializeField] private MonoBehaviourWrapper mousedOverSlot;

        [Header("Components")]
        [SerializeField] private Image glowImage;
        
        private void Awake()
        {
            Debug.Assert(mousedOverSlot != null, nameof(mousedOverSlot) + " != null");
            Debug.Assert(glowImage != null, nameof(glowImage) + " != null");

            mousedOverSlot.ReferenceChanged += SetGlow;
        }

        private void OnDestroy()
        {
            mousedOverSlot.ReferenceChanged -= SetGlow;
        }

        public void ReceiveFragment()
        {
            Debug.Log("ReceiveFragment but not implemented.");
        }
        
        private void SetGlow()
        {
            glowImage.enabled = mousedOverSlot.Current == this;
        }
    }
}