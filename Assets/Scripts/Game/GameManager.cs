using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager m_instance = null;

    static WaveManager m_waveManager;

    void Awake () {
        if (m_instance == null) {
            m_instance = this;
        } else {
            Destroy( this );
            return;
        }

        m_waveManager = new WaveManager();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        m_waveManager.Update();

    }
}
