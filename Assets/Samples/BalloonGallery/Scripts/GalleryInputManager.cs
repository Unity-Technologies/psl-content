using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class GalleryInputManager : MonoBehaviour
    {
        [SerializeField]
        Transform m_InputAxisTransform;

        [SerializeField]
        InputActionReference m_WorldTouch;

        [SerializeReference]
        InputActionReference m_WorldTouchPhase;

        void Start()
        {
            m_WorldTouch.action.Enable();
            m_WorldTouchPhase.action.Enable();
        }

        void Update()
        {
            var worldTouch = m_WorldTouch.action.ReadValue<WorldTouchState>();
            var worldTouchPhase = m_WorldTouchPhase.action.ReadValue<TouchPhase>();

            if (worldTouchPhase == TouchPhase.Began)
            {
                // only pop with poke (0) or indirect pinch (2)
                if (worldTouch.interactionKind == 0 || worldTouch.interactionKind == 2)
                {
                    var balloonObject = worldTouch.colliderObject;
                    if (balloonObject != null)
                    {
                        if(balloonObject.TryGetComponent(out BalloonBehavior balloon))
                        {
                            balloon.Pop();
                        }
                    }
                }

                m_InputAxisTransform.position = worldTouch.worldPosition;
            }

            // update debug gizmo
            if (worldTouchPhase == TouchPhase.Moved)
            {
                m_InputAxisTransform.position = worldTouch.worldPosition;
                m_InputAxisTransform.rotation = worldTouch.deviceRotation;
            }
        }
    }
}
