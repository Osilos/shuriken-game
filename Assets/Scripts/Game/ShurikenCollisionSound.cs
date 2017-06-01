using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenCollisionSound : MonoBehaviour {
    [SerializeField]
    private SM_SFX m_BulletHitSound = SM_SFX.bullet_hit_dirt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            OnBulletHit();
        }
    }
    
    private void OnBulletHit()
    {
        GameManager.instance.soundManager.PlaySFXOnTarget(m_BulletHitSound, gameObject);
    }
}
