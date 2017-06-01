using UnityEngine;

public class ShurikenCollisionSound: MonoBehaviour {
    [SerializeField]
    private SM_SFX m_BulletHitSound = SM_SFX.bullet_hit_dirt;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("bullet")) OnBulletHit();
    }
    
    private void OnBulletHit() {
        GameManager.instance.soundManager.PlaySFXOnTarget(m_BulletHitSound, transform.position);
    }
}