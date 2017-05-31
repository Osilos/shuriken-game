using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField]
    private GameObject prefabEnemy;

    private List<List<StepWave>> waves = new List<List<StepWave>>();
    private List<List<StepWave>> wavesToPlay = new List<List<StepWave>>();
    
    private void Start ()
    {
        waves = GetComponents<WaveSettings>().ToList().Select(x => x.steps).ToList();
        wavesToPlay = new List<List<StepWave>>(waves);
        Play();
    }

    private void Play ()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine ()
    {
        if (wavesToPlay.Count == 0)
            wavesToPlay = new List<List<StepWave>>(waves);

        wavesToPlay = wavesToPlay.OrderBy(x => Random.Range(0f, 1f)).ToList();
        List<StepWave> wave = wavesToPlay[0];
        wavesToPlay.Remove(wave);
        PlayWave(wave);
        yield return new WaitForSeconds(wave.Select(x => x.timeBeforeSpawn).ToList().Sum());
        Play();
    }

    private void PlayWave (List<StepWave> steps)
    {
        Wave.Create()
            .SetOrigin(Vector3.zero)
            .SetEnemyPrefab(prefabEnemy)
            .SetSteps(steps.ToArray());
    }
}
