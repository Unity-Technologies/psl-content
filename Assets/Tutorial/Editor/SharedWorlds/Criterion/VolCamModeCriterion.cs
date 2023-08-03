using Unity.PolySpatial;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.Bubblegum.Tutorial
{
    class VolCamModeCriterion : Criterion
    {
        VolumeCamera m_VolCam;

        public override void StartTesting()
        {
            m_VolCam = FindObjectOfType<VolumeCamera>();

            base.StartTesting();
            UpdateCompletion();
            EditorApplication.update += UpdateCompletion;
        }

        public override void StopTesting()
        {
            base.StopTesting();
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            if (m_VolCam == null)
                return true;
            return m_VolCam.Mode == VolumeCamera.PolySpatialVolumeCameraMode.Bounded;
        }

        public override bool AutoComplete()
        {
            m_VolCam.Mode = VolumeCamera.PolySpatialVolumeCameraMode.Bounded;

            return true;
        }
    }
}
