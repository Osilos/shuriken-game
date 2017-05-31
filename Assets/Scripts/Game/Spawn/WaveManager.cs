using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField]
    private GameObject prefabEnemy;

    private List<List<StepWave>> waves = new List<List<StepWave>>();
    private List<List<StepWave>> wavesToPlay = new List<List<StepWave>>();
    private int numberEnemy = 0;
    private int numberDeathForNextWave;

    private void Start ()
    {
        waves = GetComponents<WaveSettings>().ToList().Select(x => x.steps).ToList();
        wavesToPlay = new List<List<StepWave>>(waves);
    }

    private void OnEnemyDestroy (Enemy enemy)
    {
        numberEnemy++;
        if (numberEnemy >= numberDeathForNextWave)
            Play();
    }

    public void Play ()
    {
        Enemy.onEnemyDie.AddListener(OnEnemyDestroy);
        PlayRoutine();
    }

    public void Stop ()
    {
        Enemy.onEnemyDie.RemoveListener(OnEnemyDestroy);
        numberEnemy = 0;
    }

    private void PlayRoutine ()
    {
        if (wavesToPlay.Count == 0)
            wavesToPlay = new List<List<StepWave>>(waves);

        wavesToPlay = wavesToPlay.OrderBy(x => Random.Range(0f, 1f)).ToList();
        List<StepWave> wave = wavesToPlay[0];
        wavesToPlay.Remove(wave);
        numberDeathForNextWave = wave.Count;
        PlayWave(wave);
    }

    private void PlayWave (List<StepWave> steps)
    {
        Wave.Create()
            .SetOrigin(Vector3.zero)
            .SetEnemyPrefab(prefabEnemy)
            .SetSteps(steps.ToArray());
    }
}
