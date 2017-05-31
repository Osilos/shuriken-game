using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Camera mainCamera;

	static GameManager m_instance = null;

	static Wave m_waveManager;

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
		Wave.Create()
			.SetOrigin(Vector3.zero)
			.SetEnemyPrefab(EnemyA)
			.SetSteps(
				new StepWave[3] {
					new StepWave(new Vector3(0f,0f,5f), 0f),
					new StepWave(new Vector3(-5f,0f, 5f), 0.5f),
					new StepWave(new Vector3(5f,0f,5f), 0f)
				}
			);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//m_waveManager.Update();
	}
}
