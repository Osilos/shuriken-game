using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

    [SerializeField]
    private float m_resetTime = 3;

    private Vector3 m_initialPosition;
    private Quaternion m_initialRotation;
    private Rigidbody m_cachedBody;
    private SphereCollider m_cachedCollider;

    private bool m_reseting = false;

    

	void Awake () {
        m_reseting = false;
        m_initialRotation = transform.rotation;
        m_initialPosition = transform.position;
        m_cachedBody      = GetComponent<Rigidbody>();
        m_cachedCollider  = GetComponent<SphereCollider>();
        GameManager.instance.onGameOver.AddListener(Reset);
    }
	

    void OnCollisionEnter (Collision coll)
    {
        if (!m_reseting)
        {
            GameManager.instance.PlayerWantToRestart();
            m_cachedBody.AddForce(coll.relativeVelocity * 10, ForceMode.Impulse);
        }
    }


    void Reset ()
    {
        m_reseting = true;
        m_cachedCollider.enabled = false;
        m_cachedBody.velocity = Vector3.zero;
        m_cachedBody.angularVelocity = Vector3.zero;
        StartCoroutine(CoroutineReset());
    }


    IEnumerator CoroutineReset ()
    {
        float startTime = Time.time;
        float endTime   = Time.time + m_resetTime;
        Vector3 startPosition    = transform.position;
        Quaternion startRotation = transform.rotation;

        while (Time.time < endTime)
        {
            float timeSpent = Time.time - startTime;
            float percent   = timeSpent / m_resetTime;

            transform.position = Vector3.Lerp(startPosition, m_initialPosition, percent);
            transform.rotation = Quaternion.Slerp(startRotation, m_initialRotation, percent);

            yield return null;
        }

        m_reseting = false;
        m_cachedCollider.enabled = true;
    }
}
