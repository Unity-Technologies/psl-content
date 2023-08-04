using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class InputDebuggingHelper : MonoBehaviour
    {
        [SerializeField] List<DebugTouchHelper> m_Touches;

        [SerializeField] InputActionReference m_TouchZeroValue;

        [SerializeField] InputActionReference m_TouchZeroPhase;

        [SerializeField] InputActionReference m_TouchOneValue;

        [SerializeField] InputActionReference m_TouchOnePhase;

        [SerializeField] Transform m_TouchZeroInputGizmo;

        [SerializeField] Transform m_TouchOneInputGizmo;

        private const string k_FloatFormat = "00.00";

        void Start()
        {
            m_TouchZeroValue.action.Enable();
            m_TouchZeroPhase.action.Enable();
            m_TouchOneValue.action.Enable();
            m_TouchOnePhase.action.Enable();
        }

        void Update()
        {
            // get values
            var touchZeroValue = m_TouchZeroValue.action.ReadValue<WorldTouchState>();
            var touchZeroPhase = m_TouchZeroPhase.action.ReadValue<TouchPhase>();

            var touchOneValue = m_TouchOneValue.action.ReadValue<WorldTouchState>();
            var touchOnePhase = m_TouchOnePhase.action.ReadValue<TouchPhase>();

            // set UI values
            SetTextValues(m_Touches[0], touchZeroValue, touchZeroPhase);
            SetTextValues(m_Touches[1], touchOneValue, touchOnePhase);

            // show input visualization
            if (touchZeroPhase == TouchPhase.Began || touchZeroPhase == TouchPhase.Moved)
            {
                m_TouchZeroInputGizmo.position = touchZeroValue.worldPosition;
                m_TouchZeroInputGizmo.rotation = touchZeroValue.deviceRotation;
            }

            if (touchOnePhase == TouchPhase.Began || touchOnePhase == TouchPhase.Moved)
            {
                m_TouchOneInputGizmo.position = touchOneValue.worldPosition;
                m_TouchOneInputGizmo.rotation = touchOneValue.deviceRotation;
            }
        }

        void SetTextValues(DebugTouchHelper debugTouch, WorldTouchState touchState, TouchPhase touchPhase)
        {
            var touchPosition = touchState.worldPosition;
            var touchDevicePosition = touchState.devicePosition;
            var touchDeviceRotation = touchState.deviceRotation.eulerAngles;
            var touchSelectionRayPosition = touchState.selectionRayOrigin;
            var touchSelectionRayDirection = touchState.selectionRayDirection;

            var TouchType = new
            {
                touch = 0,
                directPinch = 1,
                indirectPinch = 2,
                pointer = 3
            };

            debugTouch.InputTypeValue.text = ((TouchType)touchState.interactionKind).ToString();
            debugTouch.PhaseValue.text = touchPhase.ToString();
            debugTouch.ColliderIDValue.text = touchState.colliderObject != null ? touchState.colliderObject.name : "None";
            debugTouch.XValue.text = touchPosition.x.ToString(k_FloatFormat);
            debugTouch.YValue.text = touchPosition.y.ToString(k_FloatFormat);
            debugTouch.ZValue.text = touchPosition.z.ToString(k_FloatFormat);
            debugTouch.DevicePositionXValue.text = touchDevicePosition.x.ToString(k_FloatFormat);
            debugTouch.DevicePositionYValue.text = touchDevicePosition.y.ToString(k_FloatFormat);
            debugTouch.DevicePositionZValue.text = touchDevicePosition.z.ToString(k_FloatFormat);
            debugTouch.DeviceRotationXValue.text = touchDeviceRotation.x.ToString(k_FloatFormat);
            debugTouch.DeviceRotationYValue.text = touchDeviceRotation.y.ToString(k_FloatFormat);
            debugTouch.DeviceRotationZValue.text = touchDeviceRotation.z.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionXValue.text = touchSelectionRayPosition.x.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionYValue.text = touchSelectionRayPosition.y.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionZValue.text = touchSelectionRayPosition.z.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationXValue.text = touchSelectionRayDirection.x.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationYValue.text = touchSelectionRayDirection.y.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationZValue.text = touchSelectionRayDirection.z.ToString(k_FloatFormat);
        }
    }

    [Serializable]
    public struct DebugTouchHelper
    {
        public TMP_Text InputTypeValue;

        public TMP_Text PhaseValue;

        public TMP_Text ColliderIDValue;

        public TMP_Text XValue;

        public TMP_Text YValue;

        public TMP_Text ZValue;

        public TMP_Text DevicePositionXValue;

        public TMP_Text DevicePositionYValue;

        public TMP_Text DevicePositionZValue;

        public TMP_Text DeviceRotationXValue;

        public TMP_Text DeviceRotationYValue;

        public TMP_Text DeviceRotationZValue;

        public TMP_Text SelectionRayPositionXValue;

        public TMP_Text SelectionRayPositionYValue;

        public TMP_Text SelectionRayPositionZValue;

        public TMP_Text SelectionRayRotationXValue;

        public TMP_Text SelectionRayRotationYValue;

        public TMP_Text SelectionRayRotationZValue;
    }
}
