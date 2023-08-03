using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.Bubblegum.Tutorial
{
    class ParticleSystemCriterion : Criterion
    {
        ParticleSystem.MainModule m_ParticleSystemModule;

        public override void StartTesting()
        {
            var particleSystem = FindObjectOfType<ParticleSystem>();
            if (particleSystem != null)
                m_ParticleSystemModule = particleSystem.main;

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
            return m_ParticleSystemModule.startRotation3D == false;
        }

        public override bool AutoComplete()
        {
            m_ParticleSystemModule.startRotation3D = false;
            return true;
        }
    }
}
