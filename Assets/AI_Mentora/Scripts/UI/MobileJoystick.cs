using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIMentora
{
    /// <summary>
    /// On-screen joystick for mobile touch input.
    /// Attach this to a UI Image that serves as the joystick background.
    /// </summary>
    public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Header("Joystick Settings")]
        [Tooltip("The movable handle inside the joystick")]
        public RectTransform Handle;

        [Tooltip("Maximum distance the handle can move from center")]
        public float HandleRange = 50f;

        [Tooltip("Dead zone - input below this threshold is ignored")]
        [Range(0f, 0.5f)]
        public float DeadZone = 0.1f;

        private RectTransform m_Background;
        private Vector2 m_InputVector;
        private Canvas m_ParentCanvas;

        public Vector2 InputVector => m_InputVector;

        private void Awake()
        {
            m_Background = GetComponent<RectTransform>();
            m_ParentCanvas = GetComponentInParent<Canvas>();

            if (Handle == null)
            {
                Debug.LogError("MobileJoystick: Handle not assigned!");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_InputVector = Vector2.zero;
            Handle.anchoredPosition = Vector2.zero;

            // Tell player controller to stop moving
            if (GameManager.Instance?.Player != null)
            {
                GameManager.Instance.Player.SetMobileMovement(Vector2.zero);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_Background, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                // Normalize to -1 to 1 range based on background size
                Vector2 sizeDelta = m_Background.sizeDelta;
                localPoint.x = localPoint.x / (sizeDelta.x * 0.5f);
                localPoint.y = localPoint.y / (sizeDelta.y * 0.5f);

                // Clamp to unit circle
                m_InputVector = Vector2.ClampMagnitude(localPoint, 1f);

                // Apply dead zone
                if (m_InputVector.magnitude < DeadZone)
                {
                    m_InputVector = Vector2.zero;
                }

                // Move handle visual
                Handle.anchoredPosition = m_InputVector * HandleRange;

                // Send input to player controller
                if (GameManager.Instance?.Player != null)
                {
                    GameManager.Instance.Player.SetMobileMovement(m_InputVector);
                }
            }
        }

        private void OnDisable()
        {
            m_InputVector = Vector2.zero;
            if (Handle != null)
            {
                Handle.anchoredPosition = Vector2.zero;
            }
        }
    }
}
