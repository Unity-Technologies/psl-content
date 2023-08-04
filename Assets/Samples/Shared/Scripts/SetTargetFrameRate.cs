using UnityEngine;

namespace PolySpatial.Samples
{
    public class SetTargetFrameRate : MonoBehaviour
    {
        [SerializeField]
        int m_TargetFrameRate = 90;

        void Awake()
        {
            Application.targetFrameRate = m_TargetFrameRate;
        }
    }
}
