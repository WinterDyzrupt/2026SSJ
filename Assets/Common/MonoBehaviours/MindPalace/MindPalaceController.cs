using UnityEngine;

namespace Common.MonoBehaviours.MindPalace
{
    public class MindPalaceController : MonoBehaviour
    {
        public void OpenMindPalace()
        {
            SetVisibility(true);
        }

        public void CloseMindPalace()
        {
            SetVisibility(false);
        }

        private void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}