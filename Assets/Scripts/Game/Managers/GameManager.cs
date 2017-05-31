﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    PLAYING,
    WAITING
}

public class GameManager : MonoBehaviour {

	public static Camera mainCamera;

	public static GameManager instance = null;
    
	static ScoreManager m_scoreManager;

    private GameState m_state;

    public UnityEvent onGameOver = new UnityEvent();

	[Header("Enemies")]
	public Enemy PrefabEnemy;
	public GameObject EnemyA;
	public GameObject EnemyB;

	[Header("Score")]
	[SerializeField] private Score3D m_scoreGO;

    [Header("Timer")]
    [SerializeField] private Timer m_timer;
    [SerializeField] private float m_timerTotalTime;

    public FXManager fxManager;
    public SoundsManager soundManager;
    public WaveManager waveManager;

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

        m_timer.onTimerEnd.AddListener(OnTimeUp);
        Enemy.onEnemyDie.AddListener(OnEnemyDie);
    }
    

	private void GameStart ()
    {
        m_state = GameState.PLAYING;
        m_scoreManager.ResetScore();
        m_timer.LaunchTimer(m_timerTotalTime);
        waveManager.Play();
    }


    private void GameOver ()
    {
        m_state = GameState.WAITING;
        onGameOver.Invoke();
        waveManager.Stop();
    }

    
    private void OnTimeUp ()
    {
        GameOver();
    }


    public void PlayerWantToRestart ()
    {
        if (m_state != GameState.PLAYING)
        {
            GameStart();
        }
    }


	public void OnEnemyDie (Enemy enemy)
	{
		m_scoreManager.UpdateScore(enemy.ScorePoints);
	}
}
