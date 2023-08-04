using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class CharacterInputManager : MonoBehaviour
    {
        const float k_DeadZoneDistance = 0.04f;
        const float k_MaxDistance = 0.5f;
        static readonly int k_MoveSpeed = Animator.StringToHash("Speed");

        [SerializeField]
        Animator m_CharacterAnimator;

        [SerializeField]
        Transform m_CharacterTransform;

        [SerializeField]
        Transform m_TargetTransform;

        [SerializeField]
        float m_FloorYPosition = -0.2f;

        [SerializeField]
        InputActionReference m_WorldTouch;

        [SerializeField]
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

            if (worldTouchPhase == TouchPhase.Began || worldTouchPhase == TouchPhase.Moved)
            {
                // keep target position aligned with ground
                var worldPosition = worldTouch.worldPosition;
                m_TargetTransform.position = new Vector3(worldPosition.x, m_FloorYPosition, worldPosition.z);
            }

            MoveCharacter();
        }

        void MoveCharacter()
        {
            var distance = Vector3.Distance(m_CharacterTransform.position, m_TargetTransform.position);

            if (distance >= k_DeadZoneDistance)
            {
                var position = m_TargetTransform.position;
                m_CharacterTransform.position = Vector3.Lerp(m_CharacterTransform.position, position, Time.deltaTime);
                m_CharacterTransform.LookAt(position);

                var normalizedSpeedValue = distance / k_MaxDistance;

                m_CharacterAnimator.SetFloat(k_MoveSpeed, normalizedSpeedValue);
            }
            else
            {
                m_CharacterAnimator.SetFloat(k_MoveSpeed, 0.0f);
            }
        }
    }
}
