using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace HappyHarvest
{
    /// <summary>
    /// Allows zooming the camera in and out using mouse scroll wheel or pinch-to-zoom on mobile.
    /// Attach this to the same GameObject as CinemachineVirtualCamera.
    /// </summary>
    public class CameraZoom : MonoBehaviour
    {
        [Header("Zoom Settings")]
        [Tooltip("Minimum orthographic size (zoomed in)")]
        public float MinZoom = 3f;

        [Tooltip("Maximum orthographic size (zoomed out)")]
        public float MaxZoom = 20f;

        [Tooltip("How fast the zoom changes (mouse scroll)")]
        public float ZoomSpeed = 2f;

        [Tooltip("How fast the zoom changes (pinch gesture)")]
        public float PinchZoomSpeed = 0.01f;

        [Tooltip("Smoothing for zoom transitions")]
        public float ZoomSmoothing = 5f;

        private CinemachineVirtualCamera m_VirtualCamera;
        private float m_TargetZoom;
        private float m_PreviousPinchDistance;

        private void Awake()
        {
            m_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (m_VirtualCamera != null)
            {
                m_TargetZoom = m_VirtualCamera.m_Lens.OrthographicSize;
            }
        }

        private void Update()
        {
            if (m_VirtualCamera == null) return;

            // Handle mouse scroll wheel (desktop)
            if (Mouse.current != null)
            {
                float scrollInput = Mouse.current.scroll.ReadValue().y;
                if (scrollInput != 0)
                {
                    m_TargetZoom -= scrollInput * ZoomSpeed * 0.01f;
                    m_TargetZoom = Mathf.Clamp(m_TargetZoom, MinZoom, MaxZoom);
                }
            }

            // Handle pinch-to-zoom (mobile)
            if (Touchscreen.current != null && Touchscreen.current.touches.Count >= 2)
            {
                var touch0 = Touchscreen.current.touches[0];
                var touch1 = Touchscreen.current.touches[1];

                if (touch0.isInProgress && touch1.isInProgress)
                {
                    Vector2 touch0Pos = touch0.position.ReadValue();
                    Vector2 touch1Pos = touch1.position.ReadValue();
                    float currentPinchDistance = Vector2.Distance(touch0Pos, touch1Pos);

                    // Check if this is the start of a pinch
                    if (touch0.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began ||
                        touch1.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                    {
                        m_PreviousPinchDistance = currentPinchDistance;
                    }
                    else
                    {
                        // Calculate pinch delta
                        float pinchDelta = currentPinchDistance - m_PreviousPinchDistance;
                        m_TargetZoom -= pinchDelta * PinchZoomSpeed;
                        m_TargetZoom = Mathf.Clamp(m_TargetZoom, MinZoom, MaxZoom);
                        m_PreviousPinchDistance = currentPinchDistance;
                    }
                }
            }

            // Smoothly interpolate to target zoom
            float currentZoom = m_VirtualCamera.m_Lens.OrthographicSize;
            float newZoom = Mathf.Lerp(currentZoom, m_TargetZoom, Time.deltaTime * ZoomSmoothing);
            m_VirtualCamera.m_Lens.OrthographicSize = newZoom;
        }
    }
}
