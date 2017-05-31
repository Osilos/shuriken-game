using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    [SerializeField]
    private GameObject m_timer3D;
    [SerializeField]
    private float m_timerMinScale;
    private Vector3 m_timerInitialScale;
    [SerializeField]
    private float m_timerTotalTime;
    

    private void Start () {
        m_timerInitialScale = m_timer3D.transform.localScale;
        LaunchTimer(m_timerTotalTime);
	}


    private void LaunchTimer (float totalTime)
    {
        StartCoroutine(TimerCoroutine(totalTime));
    }


    private IEnumerator TimerCoroutine (float totalTime)
    {
        float startTime = Time.time;
        float endTime   = startTime + totalTime;

        while (Time.time < endTime)
        {
            float timeSpent = Time.time - startTime;
            float timeLeft  = endTime   - timeSpent;
            float percent   = timeLeft / totalTime;
            SetTimerFeedback(percent);

            yield return null;
        }
    }


    private void SetTimerFeedback (float percent)
    {
        m_timer3D.transform.localScale = m_timerInitialScale * percent;
    }
}
