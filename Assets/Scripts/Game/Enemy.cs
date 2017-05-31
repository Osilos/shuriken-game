using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyDieEvent : UnityEvent<Enemy> { }

public class Enemy : MonoBehaviour {
    

    public OnEnemyDieEvent onEnemyDie = new OnEnemyDieEvent();

    [SerializeField]
    private float m_scorePoints = 10;

    public float ScorePoints
    {
        get
        {
            return m_scorePoints;
        }
    }

    public short health;
    public byte attackTypes;

    void Awake () {
        health = 0;
        attackTypes = (byte)AttackType.Main;
    }
    

    void Update () {
        
    }

    void Start () {
        AnimPop();
    }

    void AnimPop () {

    }

    void AnimDie () {

    }

    void AnimHit () {

    }

    void AnimMiss () {

    }

    void LooseHealth () {
        if (--health <= 0) {
            AnimDie();
            onEnemyDie.Invoke(this);
        } else {
            AnimHit();
        }
    }

    public void OnHit (AttackType type = AttackType.Main) {
        if ((byte)type == attackTypes) {
            LooseHealth();
        } else {
            AnimMiss();
        }
    }

}
