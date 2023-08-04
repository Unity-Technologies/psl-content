using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class BalloonGalleryManager : MonoBehaviour
    {
        [SerializeField]
        TMP_Text m_ScoreText;

        [SerializeField]
        GameObject m_WinConfetti;

        List<GameObject> m_Balloons;

        int m_CurrentScore = 0;
        int m_NumberOfBalloonsPopped;
        int m_NumberOfGoodBalloons;

        void Awake()
        {
            m_CurrentScore = 0;
            var balloons = FindObjectsOfType<BalloonBehavior>();

            foreach (var balloon in balloons)
            {
                balloon.m_Manager = this;
            }
        }

        public void BalloonPopped(int scoreValue)
        {
            m_CurrentScore += scoreValue;
            m_ScoreText.text = m_CurrentScore.ToString();
            // good balloon
            if (scoreValue > 0)
            {
                m_NumberOfBalloonsPopped++;
            }

            // all good balloons popped, Show confetti
            if (m_NumberOfBalloonsPopped == m_NumberOfGoodBalloons)
            {
                Instantiate(m_WinConfetti);
            }
        }

        public void GoodBalloonAdded()
        {
            m_NumberOfGoodBalloons++;
        }
    }
}
