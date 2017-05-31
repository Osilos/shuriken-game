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
    }


    public void UpdateScore (float scoreGained)
    {
        m_currentScore += scoreGained;
        m_scoreFeedback.SetScore(m_currentScore);
    }

}
