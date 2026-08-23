
using System;
using Common.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours.MindPalace
{
    public class FragmentDropSlot : MonoBehaviour
    {
        [Header("Wrappers")]
        [SerializeField] private FragmentDropSlotWrapper mousedOverSlot;

        [Header("Components")]
        [SerializeField] private Image glowImage;
        
        private void Awake()
        {
            Debug.Assert(mousedOverSlot != null, nameof(mousedOverSlot) + " != null");
            Debug.Assert(glowImage != null, nameof(glowImage) + " != null");

            mousedOverSlot.Changed += SetGlow;
        }

        private void OnDestroy()
        {
            mousedOverSlot.Changed -= SetGlow;
        }

        public void DropFragment()
        {
            Debug.Log("Fragment dropped but not implemented.");
        }
        
        private void SetGlow()
        {
            glowImage.enabled = mousedOverSlot.CurrentDropSlot == this;
        }
    }
}