using System;
using Common.Data;
using TMPro;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class InfoPanel : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TMP_Text displayNameText;
        [SerializeField] private TMP_Text descriptionText;

        [Header("Wrappers")]
        [SerializeField] private MonoBehaviourWrapper mousedOverFragment;

        private void Awake()
        {
            Debug.Assert(displayNameText != null, nameof(displayNameText) + " != null");
            Debug.Assert(descriptionText != null, nameof(descriptionText) + " != null");
            Debug.Assert(mousedOverFragment != null, nameof(mousedOverFragment) + " != null");

            mousedOverFragment.ReferenceChanged += UpdateText;
        }

        private void OnDestroy()
        {
            mousedOverFragment.ReferenceChanged -= UpdateText;
        }

        private void UpdateText()
        {
            var targetFragment = mousedOverFragment.Current as DraggableFragment;
            displayNameText.text = targetFragment ? targetFragment.Data.displayName : string.Empty;
            descriptionText.text = targetFragment ? targetFragment.Data.description : string.Empty;
        }
    }
}