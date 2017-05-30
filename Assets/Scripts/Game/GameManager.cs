using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static Camera mainCamera;

    static GameManager m_instance = null;

    static WaveManager m_waveManager;

    [Header("Enemies")]
    public Enemy PrefabEnemy;
    public GameObject EnemyA;
    public GameObject EnemyB;

    void Awake () {
        if (m_instance == null) {
            m_instance = this;
        } else {
            Destroy( this );
            return;
        }

        mainCamera = Camera.main;
        m_waveManager = new WaveManager( PrefabEnemy, EnemyA, EnemyB );
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        m_waveManager.Update();
    }
}
