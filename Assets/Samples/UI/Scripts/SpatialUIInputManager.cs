using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class SpatialUIInputManager : MonoBehaviour
    {
        [SerializeField]
        InputActionReference m_WorldTouch;

        [SerializeReference]
        InputActionReference m_WorldTouchPhase;

        private void Start()
        {
            m_WorldTouch.action.Enable();
            m_WorldTouchPhase.action.Enable();
        }

        private void Update()
        {
            var worldTouch = m_WorldTouch.action.ReadValue<WorldTouchState>();
            var worldTouchPhase = m_WorldTouchPhase.action.ReadValue<TouchPhase>();

            if (worldTouchPhase == TouchPhase.Began)
            {
                var buttonObject = worldTouch.colliderObject;
                if (buttonObject != null)
                {
                    if(buttonObject.TryGetComponent(out SpatialUI button))
                    {
                        button.Press(worldTouch.worldPosition);
                    }
                }
            }

            // special case for sliders
            if (worldTouchPhase == TouchPhase.Moved)
            {
                var sliderObject = worldTouch.colliderObject;
                if (sliderObject != null)
                {
                    if(sliderObject.TryGetComponent(out SpatialUISlider slider))
                    {
                        slider.Press(worldTouch.worldPosition);
                    }
                }
            }
        }
    }
}
