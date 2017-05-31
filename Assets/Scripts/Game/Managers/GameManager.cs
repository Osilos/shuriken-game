using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Camera mainCamera;

	static GameManager m_instance = null;

	static Wave m_waveManager;

	[Header("Enemies")]
	public Ennemy PrefabEnemy;
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
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//m_waveManager.Update();
	}
}
