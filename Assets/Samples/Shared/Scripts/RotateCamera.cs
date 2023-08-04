using UnityEngine;

namespace PolySpatial.Samples
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField]
        Transform m_CameraTarget;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(m_CameraTarget);

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(Vector3.left * Time.deltaTime * 10);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(Vector3.right * Time.deltaTime * 10);
            }
        }
    }
}
