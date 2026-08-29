using System;
using Common.Data;
using TMPro;
using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class InfoPanel : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TMP_Text subjectText;
        [SerializeField] private TMP_Text descriptionText;

        [Header("Wrappers")]
        [SerializeField] private MonoBehaviourWrapper mousedOverFragment;

        private void Awake()
        {
            Debug.Assert(subjectText != null, nameof(subjectText) + " != null");
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
            subjectText.text = targetFragment ? targetFragment.FragmentData.subject : string.Empty;
            descriptionText.text = targetFragment ? targetFragment.FragmentData.description : string.Empty;
        }
    }
}