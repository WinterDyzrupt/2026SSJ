using System;
using Common.Data.Dialog;
using Common.MonoBehaviours;
using UnityEngine;

namespace Scene1.MonoBehaviours
{
    public class Manager : MonoBehaviour
    {
        public DialogController dialogController;
        public Script script;

        /// <summary>
        /// Placeholder logic to automatically start dialog while things-to-click-on are being developed.
        /// </summary>
        private DateTime statTime;
        private TimeSpan timeToWaitBeforeStartingDialog = TimeSpan.FromSeconds(2);
        private bool isDialogStartedInProgress;
        private bool isIntroDialogStarted;
        private bool isTutorialDialogStarted;
        private bool isOtherTextDialogStarted;
        private DateTime dialogCompletionTime;
        
        private void Start()
        {
            // Placeholder logic to automatically start dialog while things-to-click-on are being developed.
            dialogCompletionTime = DateTime.Now;
        }

        private void Update()
        {
            // Placeholder logic to automatically start dialog while things-to-click-on are being developed.
            if (DateTime.Now - dialogCompletionTime > timeToWaitBeforeStartingDialog && !isDialogStartedInProgress)
            {
                isDialogStartedInProgress = true;

                if (!isIntroDialogStarted)
                {
                    dialogController.StartDialog(script.intro);
                    isIntroDialogStarted = true;
                }
                else if (!isTutorialDialogStarted)
                {
                    dialogController.StartDialog(script.tutorial);
                    isTutorialDialogStarted = true;
                }
                else if (!isOtherTextDialogStarted)
                {
                    dialogController.StartDialog(script.otherText);
                    isOtherTextDialogStarted = true;
                }
            }

            // Placeholder logic to keep track of when a dialog finishes
            if (isDialogStartedInProgress && !dialogController.isDialogInProgress)
            {
                dialogCompletionTime = DateTime.Now;
                isDialogStartedInProgress = false;
            }
        }
    }
}
