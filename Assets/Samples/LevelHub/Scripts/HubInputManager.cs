using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class HubInputManager : MonoBehaviour
    {
        [SerializeField]
        InputActionReference m_WorldTouch;

        [SerializeField]
        InputActionReference m_WorldTouchPhase;

        TouchPhase m_LastTouchPhase;

        void Start()
        {
            m_WorldTouch.action.Enable();
            m_WorldTouchPhase.action.Enable();
        }

        void Update()
        {
            var worldTouch = m_WorldTouch.action.ReadValue<WorldTouchState>();
            var worldTouchPhase = m_WorldTouchPhase.action.ReadValue<TouchPhase>();

            if (worldTouchPhase == TouchPhase.Began && m_LastTouchPhase != TouchPhase.Began)
            {
                var buttonObject = worldTouch.colliderObject;
                if (buttonObject != null)
                {
                    if(buttonObject.TryGetComponent(out HubButton button))
                    {
                        button.Press();
                    }
                }
            }

            m_LastTouchPhase = worldTouchPhase;
        }
    }
}
