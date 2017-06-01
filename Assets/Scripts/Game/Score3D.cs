using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score3D : MonoBehaviour {

    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private Transform m_showToPlayerMark;
    [SerializeField]
    private float m_animDuration;
    [SerializeField]
    private AnimationCurve m_animCurve;

    private Vector3 m_initialPosition;
    private Quaternion m_initialRotation;


    private void Start () {
        m_initialPosition = transform.position;
        m_initialRotation = transform.rotation;
        DisplayScore("0");
	}


    public void SetScore (float score)
    {
        DisplayScore(score.ToString());
    }


    private void DisplayScore (string score)
    {
        m_scoreText.text = score;
    }


    public void ShowToPlayer ()
    {
        StartCoroutine(CoroutineMove(m_showToPlayerMark.position, m_showToPlayerMark.rotation, m_animDuration));
    }


    public void ReturnToInitialPosition ()
    {
        StartCoroutine(CoroutineMove(m_initialPosition, m_initialRotation, m_animDuration));
    }


    IEnumerator CoroutineMove (Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        float startTime = Time.time;
        float endTime   = startTime + duration;

        Vector3 initialPosition    = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (Time.time < endTime)
        {
            float timeSpent = Time.time - startTime;
            float percent   = timeSpent / duration;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, m_animCurve.Evaluate(percent));
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, percent);
            
            yield return null;
        }

    }
}
