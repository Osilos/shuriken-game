using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPosition : MonoBehaviour {

    [SerializeField]
    private GameObject m_HMD;
    private Vector3 m_HMDOffset;

	// Use this for initialization
	void Awake () {
        m_HMDOffset = m_HMD.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = m_HMD.transform.position - m_HMDOffset;
	}
}
