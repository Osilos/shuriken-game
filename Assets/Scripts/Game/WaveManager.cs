using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager {

    Enemy m_enemyWrapperPrefab;
    GameObject m_enemyAPrefab;
    GameObject m_enemyBPrefab;

    SphericalCoordinates m_sphericalTransform;

    public WaveManager (Enemy enemyWrapperPrefab, GameObject enemyAPrefab, GameObject enemyBPrefab) {
        m_enemyWrapperPrefab = enemyWrapperPrefab;
        m_enemyAPrefab = enemyAPrefab;
        m_enemyBPrefab = enemyBPrefab;

        m_sphericalTransform = new SphericalCoordinates( new Vector3(0f, 0f, 0f), 1f, 20f, -(Mathf.PI * 2f), Mathf.PI * 2f, 0f, Mathf.PI / 2f );

        PlayWave(new Vector3[3] {
            new Vector3(0f, 0f, 0f),
            new Vector3(180f, 180f, 0f),
            new Vector3(360f, 360f, 0f)
        } );
    }

    public void Update () {

    }

    Enemy InstantiateEnemy () {

        Enemy enemy = GameObject.Instantiate(m_enemyWrapperPrefab);
        GameObject gfx = GameObject.Instantiate( Random.Range( 0, 1 ) == 0 ? m_enemyAPrefab : m_enemyBPrefab );
        gfx.transform.SetParent( enemy.body.transform );
        return enemy;
    }

    void PlayWave (Vector3[] enemies) {

        Enemy enemy;

        for (int i = 0; i < enemies.Length; ++i) {
            enemy = InstantiateEnemy();
            
            m_sphericalTransform.FromCartesian( enemies[i] );
            m_sphericalTransform.SetRadius( enemies[i].z );
            enemy.gameObject.transform.position = m_sphericalTransform.toCartesian;

            enemy.gameObject.transform.LookAt( new Vector3( 0f, 0f, 0f ) );
        }
    }
}
