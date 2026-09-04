using System;
using Common.Data;
using Common.Data.Dialog;
using Common.MonoBehaviours;
using UnityEngine;

namespace Scene1.MonoBehaviours
{
    public class Manager : MonoBehaviour
    {
        public DialogController dialogController;
        public Script script;
        public BoolWrapper cluesAreInteractable;

        /// <summary>
        /// Placeholder logic to automatically start dialog while things-to-click-on are being developed.
        /// </summary>
        private DateTime _startTime;
        private TimeSpan _timeToWaitBeforeStartingIntro = TimeSpan.FromSeconds(2);
        private bool _introDialogStarted;

        public void Awake()
        {
            Debug.Assert(dialogController != null, nameof(DialogController) + " expected to be non-null.");
            Debug.Assert(script != null, nameof(Script) + " expected to be non-null.");
            Debug.Assert(cluesAreInteractable != null, nameof(cluesAreInteractable) + " expected to be non-null.");
        }
        
        private void Start()
        {
            cluesAreInteractable.Set(false);
            // Used to automatically start the intro dialog.
            _startTime = DateTime.Now;
        }

        private void Update()
        {
            // Automatically start the intro dialog after a short delay.
            if (!_introDialogStarted && DateTime.Now - _startTime > _timeToWaitBeforeStartingIntro)
            {
                _introDialogStarted = true;
                dialogController.StartDialog(script.intro);
            }

            // Intro or subsequent dialog ended.
            if (_introDialogStarted && !dialogController.isDialogInProgress && !cluesAreInteractable)
            {
                Debug.Log("Enabling clues now that a dialog is not in progress.");
                cluesAreInteractable.Set(true);
            }
            
        }

        public void OnClue1Clicked()
        {
            Debug.Log("OnClue1Clicked: Disabling clues and starting dialog");
            dialogController.StartDialog(script.otherText);
        }
    }
}
