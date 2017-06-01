using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ScoreManager
{
    private float m_currentScore = 0;
    private float m_bestScore    = 0;

    private Score3D m_scoreFeedback;

    public float Score
    {
        get
        {
            return m_currentScore;
        }
    }

    public float BestScore
    {
        get
        {
            return m_bestScore;
        }
    }


    public ScoreManager(Score3D scoreGO)
    {
        m_scoreFeedback = scoreGO;
        GameManager.instance.onGameOver.AddListener(ShowScore);
    }


    public void UpdateScore (float scoreGained)
    {
        m_currentScore += scoreGained;
        m_scoreFeedback.SetScore(m_currentScore);

        m_bestScore = m_currentScore > m_bestScore ? m_currentScore : m_bestScore;
    }


    public void ResetScore ()
    {
        m_currentScore = 0;
        m_scoreFeedback.SetScore(m_currentScore);
        m_scoreFeedback.ReturnToInitialPosition();
    }


    private void ShowScore ()
    {
        m_scoreFeedback.ShowToPlayer();
    }

}
