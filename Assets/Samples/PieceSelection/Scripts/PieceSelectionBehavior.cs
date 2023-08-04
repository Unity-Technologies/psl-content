using UnityEngine;

namespace PolySpatial.Samples
{
    [RequireComponent(typeof(Rigidbody))]
    public class PieceSelectionBehavior : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer m_MeshRenderer;

        [SerializeField]
        Material m_DefaultMat;

        [SerializeField]
        Material m_SelectedMat;

        Rigidbody m_Rigidbody;

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Select(bool selected)
        {
            m_MeshRenderer.material = selected ? m_SelectedMat : m_DefaultMat;
            m_Rigidbody.isKinematic = selected;
        }
    }
}
