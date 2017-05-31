using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static Camera mainCamera;

    static GameManager m_instance = null;

    static WaveManager m_waveManager;
    static ScoreManager m_scoreManager;

    [Header("Enemies")]
    public Enemy PrefabEnemy;
    public GameObject EnemyA;
    public GameObject EnemyB;

    [Header("Score")]
    [SerializeField] private Score3D m_scoreGO;

    void Awake () {
        if (m_instance == null) {
            m_instance = this;
        } else {
            Destroy( this );
            return;
        }

        mainCamera = Camera.main;
        m_waveManager = new WaveManager( PrefabEnemy, EnemyA, EnemyB );
        if (m_scoreGO != null)
        {
            m_scoreManager = new ScoreManager(m_scoreGO);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        m_waveManager.Update();
    }
}
