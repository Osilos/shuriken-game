using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyDieEvent : UnityEvent<Enemy> { }

[RequireComponent(typeof(Collider))]
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

    private GameManager gameManager;

    void Awake () {
        attackTypes = (byte)AttackType.Main;
    }

    private void Start ()
    {
        gameManager = GameManager.instance;
        gameManager.fxManager.PlayOnce(FM_FX.FX_Spawn, null, transform.position);
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("bullet"))
            OnHit();
    }

    void AnimDie () {
        
        gameManager.fxManager.PlayOnce(FM_FX.FX_Fireworks_Green_Small, null, transform.position);
        //NICOLAS
        //gameManager.soundManager.PlaySfx(SM_SFX)
        Destroy(gameObject);
    }

    void LooseHealth () {
        if (--health <= 0) {
            AnimDie();
            onEnemyDie.Invoke(this);
        }
    }

    public void OnHit (AttackType type = AttackType.Main) {
        if ((byte)type == attackTypes) {
            LooseHealth();
        }
    }

}
