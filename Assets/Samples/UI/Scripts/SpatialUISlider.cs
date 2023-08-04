using UnityEngine;
using UnityEngine.UI;

namespace PolySpatial.Samples
{
    public class SpatialUISlider : SpatialUI
    {
        [SerializeField]
        Image m_FillImage;

        float m_BoxColliderSizeX;
        Vector3 m_ColliderCenter;

        void Start()
        {
            m_BoxColliderSizeX = GetComponent<BoxCollider>().size.x;
            m_ColliderCenter = GetComponent<BoxCollider>().center;
            Debug.Log(m_ColliderCenter);
            Debug.Log(m_BoxColliderSizeX);
        }

        public override void Press(Vector3 position)
        {
            base.Press(position);
            var localPosition = transform.InverseTransformPoint(position);
            var percentage = (localPosition.x + m_BoxColliderSizeX / 2) / m_BoxColliderSizeX;
            m_FillImage.fillAmount = 1.0f - percentage;
        }
    }
}
