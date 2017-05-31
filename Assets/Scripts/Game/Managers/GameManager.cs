﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Camera mainCamera;

	public static GameManager instance = null;

	static Wave m_waveManager;
	static ScoreManager m_scoreManager;

	[Header("Enemies")]
	public Enemy PrefabEnemy;
	public GameObject EnemyA;
	public GameObject EnemyB;

	[Header("Score")]
	[SerializeField] private Score3D m_scoreGO;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy( this );
			return;
		}

		mainCamera = Camera.main;

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
		
		//m_waveManager.Update();
	}


	public void OnEnemyDie (Enemy enemy)
	{
		m_scoreManager.UpdateScore(enemy.ScorePoints);
	}
}
