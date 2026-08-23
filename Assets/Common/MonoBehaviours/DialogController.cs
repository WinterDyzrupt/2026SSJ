using System.Collections.Generic;
using Common.Data.Dialog;
using Common.Data.Dialog.Lines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.MonoBehaviours
{
    public class DialogController : MonoBehaviour
    {
        /// <summary>
        /// The dialog box and everything associated with it.  Used to toggle dialog on/off.
        /// </summary>
        public GameObject dialogOverlay;

        public TMP_Text speakerNameTextBox;

        public TMP_Text dialogTextBox;

        public Image leftParticipant;

        public Image rightParticipant;

        /// <summary>
        /// Whether a dialog is currently in progress.
        /// </summary>
        public bool isDialogInProgress;

        private ScriptChunk _currentChunk;
        private IEnumerator<Line> _lineEnumerator;
        private readonly Color _defaultColor = Color.white;

        private void Awake()
        {
            Debug.Assert(dialogOverlay != null, nameof(dialogOverlay) + " must be non-null.");
            Debug.Assert(speakerNameTextBox != null, nameof(speakerNameTextBox) + " must be non-null.");
            Debug.Assert(dialogTextBox != null, nameof(dialogTextBox) + " must be non-null.");
            Debug.Assert(leftParticipant != null, nameof(leftParticipant) + " must be non-null.");
            Debug.Assert(rightParticipant != null, nameof(rightParticipant) + " must be non-null.");

            CloseDialog();
        }

        public void CloseDialog()
        {
            Debug.Log("DialogBox.Close");
            dialogOverlay.SetActive(false);
            speakerNameTextBox.text = string.Empty;
            dialogTextBox.text = string.Empty;
            leftParticipant.color = _defaultColor;
            leftParticipant.gameObject.SetActive(false);
            rightParticipant.color = _defaultColor;
            rightParticipant.gameObject.SetActive(false);
            
            isDialogInProgress = false;
            _currentChunk = null;
        }

        public void StartDialog(ScriptChunk dialog)
        {
            Debug.Assert(dialog != null, nameof(dialog) + " must be non-null.");
            Debug.Assert(dialog.lines != null, nameof(dialog.lines) + " must be non-null.");
            Debug.Assert(dialog.lines.Count > 0, nameof(dialog.lines) + " must have at least one line/");

            if (isDialogInProgress)
            {
                Debug.LogWarning("DialogBox.StartDialog: Dialog already in progress; tried to start: " + dialog.name);
            }
            else
            {
                Debug.Log("DialogBox.StartDialog: " + dialog.name);
                isDialogInProgress = true;
                _currentChunk = dialog;

                _lineEnumerator = dialog.lines.GetEnumerator();
                OnDialogProgressed();
                dialogOverlay.SetActive(true);
            }
        }

        private void PopulateDialog(Line line)
        {
            Debug.Assert(isDialogInProgress, $"Expected {nameof(isDialogInProgress)} to be true when populating dialog.");
            Debug.Assert(_currentChunk != null, nameof(_currentChunk) + " expected to be non-null when populating dialog.");

            if (line != null)
            {
                Debug.Log("Displaying line: " + line);

                if (line.speaker?.character != null)
                {
                    speakerNameTextBox.text = line.speaker.character.displayName;
                    leftParticipant.color = line.speaker.character.color;
                    leftParticipant.gameObject.SetActive(true);
                }

                dialogTextBox.text = line.text;
            }
            else
            {
                Debug.LogWarning("PopulateDialog: Empty line should have been stopped earlier");
            }
        }

        public void OnDialogProgressed()
        {
            Debug.Assert(_lineEnumerator != null, nameof(_lineEnumerator) + " must be non-null when progressing dialog.");

            Debug.Log("DialogBox.OnClick");
            var nextLine = _lineEnumerator.MoveNext();
            if (nextLine && _lineEnumerator.Current != null)
            {
                PopulateDialog(_lineEnumerator.Current);
            }
            else
            {
                Debug.Log("Dialog is done");
                CloseDialog();
            }
        }
    }
}