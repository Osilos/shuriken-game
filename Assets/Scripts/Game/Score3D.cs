using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score3D : MonoBehaviour {

    [SerializeField]
    private Text m_ScoreText;


    private void Start () {
        DisplayScore("0");
	}


    public void SetScore (float score)
    {
        DisplayScore(score.ToString());
    }


    private void DisplayScore (string score)
    {
        m_ScoreText.text = score;
    }
}
