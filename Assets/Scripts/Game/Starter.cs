using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

    private Vector3 m_initialPosition;
    private Quaternion m_initialRotation;
    private Rigidbody m_cachedBody;

	// Use this for initialization
	void Awake () {
        m_initialRotation = transform.rotation;
        m_initialPosition = transform.position;
        m_cachedBody      = GetComponent<Rigidbody>();
        GameManager.instance.onGameOver.AddListener(Reset);
    }
	

    void OnCollisionEnter (Collision coll)
    {
        GameManager.instance.PlayerWantToRestart();
        m_cachedBody.AddForce(coll.relativeVelocity * 10, ForceMode.Impulse);
    }


    void Reset ()
    {
        m_cachedBody.velocity = Vector3.zero;
        m_cachedBody.angularVelocity = Vector3.zero;
        transform.position    = m_initialPosition;
        transform.rotation    = m_initialRotation;
    }
}
