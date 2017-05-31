using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    [SerializeField]
    private GameObject m_timer3D;
    [SerializeField]
    private float m_timerMinScale;
    private Vector3 m_timerInitialScale;

    public UnityEvent onTimerEnd;
    

    private void Awake () {
        m_timerInitialScale = m_timer3D.transform.localScale;
	}


    public void LaunchTimer (float totalTime)
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

        onTimerEnd.Invoke();
    }


    private void SetTimerFeedback (float percent)
    {
        m_timer3D.transform.localScale = m_timerInitialScale * percent;
    }
}
