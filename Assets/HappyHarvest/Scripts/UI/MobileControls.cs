using UnityEngine;
using UnityEngine.UI;

namespace HappyHarvest
{
    /// <summary>
    /// Manages mobile UI controls visibility and functionality.
    /// This component auto-shows on mobile/touch devices and hides on desktop.
    /// </summary>
    public class MobileControls : MonoBehaviour
    {
        [Header("Control References")]
        [Tooltip("The joystick background GameObject")]
        public GameObject JoystickObject;

        [Tooltip("Use/Action button")]
        public Button UseButton;

        [Tooltip("Next item button")]
        public Button NextItemButton;

        [Tooltip("Previous item button")]
        public Button PrevItemButton;

        [Header("Settings")]
        [Tooltip("Force show controls even on desktop (for testing)")]
        public bool ForceShowOnDesktop = false;

        private void Start()
        {
            // Set up button callbacks
            if (UseButton != null)
            {
                UseButton.onClick.AddListener(OnUseButtonPressed);
            }

            if (NextItemButton != null)
            {
                NextItemButton.onClick.AddListener(OnNextItemPressed);
            }

            if (PrevItemButton != null)
            {
                PrevItemButton.onClick.AddListener(OnPrevItemPressed);
            }

            // Determine if we should show mobile controls
            UpdateControlsVisibility();
        }

        private void UpdateControlsVisibility()
        {
            bool showControls = ForceShowOnDesktop || IsMobileDevice();

            if (JoystickObject != null)
                JoystickObject.SetActive(showControls);

            if (UseButton != null)
                UseButton.gameObject.SetActive(showControls);

            if (NextItemButton != null)
                NextItemButton.gameObject.SetActive(showControls);

            if (PrevItemButton != null)
                PrevItemButton.gameObject.SetActive(showControls);
        }

        private bool IsMobileDevice()
        {
#if UNITY_ANDROID || UNITY_IOS
            return true;
#else
            // Also check for touch support on other platforms
            return Input.touchSupported && !Input.mousePresent;
#endif
        }

        public void OnUseButtonPressed()
        {
            if (GameManager.Instance?.Player != null)
            {
                GameManager.Instance.Player.OnMobileUseButton();
            }
        }

        private void OnNextItemPressed()
        {
            if (GameManager.Instance?.Player != null)
            {
                GameManager.Instance.Player.OnMobileNextItem();
            }
        }

        private void OnPrevItemPressed()
        {
            if (GameManager.Instance?.Player != null)
            {
                GameManager.Instance.Player.OnMobilePrevItem();
            }
        }

        // Public method to toggle controls visibility
        public void SetControlsVisible(bool visible)
        {
            if (JoystickObject != null)
                JoystickObject.SetActive(visible);

            if (UseButton != null)
                UseButton.gameObject.SetActive(visible);

            if (NextItemButton != null)
                NextItemButton.gameObject.SetActive(visible);

            if (PrevItemButton != null)
                PrevItemButton.gameObject.SetActive(visible);
        }
    }
}
