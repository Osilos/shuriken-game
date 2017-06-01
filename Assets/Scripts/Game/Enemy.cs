using UnityEngine;
using UnityEngine.Events;

public class OnEnemyDieEvent: UnityEvent<Enemy> { }

[RequireComponent(typeof(Collider))]
public class Enemy: MonoBehaviour {
    #region Variables
    public static OnEnemyDieEvent onEnemyDie = new OnEnemyDieEvent();

    [SerializeField]
    private float m_scorePoints = 10;
    private GameManager gameManager;

    public float ScorePoints {
        get { return m_scorePoints; }
    }

    public short health;
    public byte attackTypes;
    #endregion

    #region Initialisation & Destroy
    void Awake () {
        attackTypes = (byte)AttackType.Main;
        GameManager.instance.onGameOver.AddListener(DestroyInstance);
    }

    private void Start () {
        gameManager = GameManager.instance;
        gameManager.fxManager.PlayOnce(FM_FX.FX_Spawn, null, transform.position);
        gameManager.soundManager.PlaySFXOnTarget((SM_SFX)Random.Range(4, 6), transform.position);
    }

    private void DestroyInstance() {
        GameManager.instance.onGameOver.RemoveListener(DestroyInstance);

        gameManager = null;

        Destroy(gameObject);
    }
    #endregion

    #region Enemy Managment
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("bullet")) OnHit();
    }

    public void OnHit(AttackType type = AttackType.Main) {
        if ((byte)type == attackTypes) LooseHealth();
    }

    void LooseHealth() {
        if (--health <= 0) {
            AnimDie();
            onEnemyDie.Invoke(this);
        }
    }

    void AnimDie () {
        gameManager.fxManager.PlayOnce(FM_FX.FX_Fireworks_Green_Small, null, transform.position);
        gameManager.soundManager.PlaySFXOnTarget(SM_SFX.enemy_die_1, transform.position);
        DestroyInstance();
    }
    #endregion
}