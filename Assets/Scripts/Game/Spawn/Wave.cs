using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    private StepWave[] steps;
    private GameObject ennemyPrefab;
    private SphericalCoordinates sphericalCoordinates;
    private Vector3 origin;

    public static Wave Create ()
    {
        return new GameObject("Wave").AddComponent<Wave>();
    }

    public Wave SetOrigin(Vector3 position)
    {
        this.origin = position;
        return SetSphericalCoordinates(position);
    }

    public Wave SetEnemyPrefab (GameObject ennemyPrefab)
    {
        this.ennemyPrefab = ennemyPrefab;
        return this;
    }

    public Wave SetSteps (StepWave[] steps)
    {
        this.steps = steps;
        return this;
    }

    private Wave SetSphericalCoordinates(Vector3 position)
    {
        this.sphericalCoordinates = 
            new SphericalCoordinates(
                position, 
                1f, 
                20f, 
                -(Mathf.PI * 2f), 
                Mathf.PI * 2f, 
                0f, 
                Mathf.PI / 2f
            );
        return this;
    }

    public void Start ()
    {
        StartCoroutine(PlaySteps());
    }

    private GameObject InstantiateEnemy ()
    {
        GameObject enemy = Instantiate(ennemyPrefab);
        //enemy.GetComponent<Enemy>().onEnemyDie.AddListener(OnEnemyDie);
        return enemy;
    }


    private void OnEnemyDie (Enemy enemy)
    {
        GameManager.instance.OnEnemyDie(enemy);
    }


    private IEnumerator PlaySteps ()
    {
        GameObject ennemy;
        Transform mainCamera = GameManager.mainCamera.transform;
        int count = steps.Length;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(steps[i].time);
            ennemy = InstantiateEnemy();

            sphericalCoordinates.FromCartesian(steps[i].position);
            sphericalCoordinates.SetRadius(steps[i].position.z);

            ennemy.gameObject.transform.position = sphericalCoordinates.toCartesian;
            ennemy.gameObject.transform.LookAt(origin);
        }

        Destroy(gameObject);
    }
}
