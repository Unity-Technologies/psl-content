using System;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class CameraMovementBehavior : MonoBehaviour
    {
        [SerializeField]
        Transform m_VolumeCameraTransfrom;

        [SerializeField]
        Transform m_CharacterTransform;

        [SerializeField]
        float m_CameraDistanceThreshold = 0.3f;

        float m_LerpSpeed = 1.0f;
        float m_DistanceToAxis;
        float m_StartTime;
        Vector3 m_CurrentCameraPosition;
        bool m_Lerping;

        void Update()
        {
            var distance = Vector3.Distance(m_VolumeCameraTransfrom.position, m_CharacterTransform.position);

            if (distance >= m_CameraDistanceThreshold)
            {
                if (!m_Lerping)
                {
                    m_StartTime = Time.time;
                    m_DistanceToAxis = distance;
                    m_CurrentCameraPosition = m_VolumeCameraTransfrom.position;
                    m_Lerping = true;
                }

                var distanceCovered = (Time.time - m_StartTime) * m_LerpSpeed;
                var fractionOfLerp = distanceCovered / m_DistanceToAxis;
                m_VolumeCameraTransfrom.position = Vector3.Lerp(m_CurrentCameraPosition, m_CharacterTransform.position, fractionOfLerp);
            }
            else
            {
                m_Lerping = false;
            }

        }
    }
}
