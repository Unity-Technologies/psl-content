using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    /// <summary>
    /// Current you can only select one object at a time and only supports a primary [0] touch
    /// </summary>
    public class ManipulationInputManager : MonoBehaviour
    {
        [SerializeField]
        InputActionReference m_WorldTouch;

        [SerializeReference]
        InputActionReference m_WorldTouchPhase;

        PieceSelectionBehavior m_CurrentSelection;

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
                if (worldTouch.interactionKind == 1 || worldTouch.interactionKind == 2)
                {
                    var pieceObject = worldTouch.colliderObject;
                    if (pieceObject != null)
                    {
                        if(pieceObject.TryGetComponent(out PieceSelectionBehavior piece))
                        {
                            m_CurrentSelection = piece;
                            m_CurrentSelection.Select(true);
                        }
                    }
                }
            }

            if(worldTouchPhase == TouchPhase.Moved)
            {
                if (m_CurrentSelection != null)
                {
                    m_CurrentSelection.transform.SetPositionAndRotation(worldTouch.worldPosition, worldTouch.deviceRotation);
                }
            }

            if(worldTouchPhase == TouchPhase.Ended || worldTouchPhase == TouchPhase.Canceled)
            {
                if (m_CurrentSelection != null)
                {
                    m_CurrentSelection.Select(false);
                    m_CurrentSelection = null;
                }
            }
        }
    }
}
