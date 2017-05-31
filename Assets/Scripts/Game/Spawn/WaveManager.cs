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
        StartCoroutine(Play());
    }

    private IEnumerator Play ()
    {
        if (wavesToPlay.Count != 0)
        {
            wavesToPlay = wavesToPlay.OrderBy(x => Random.Range(0, 1)).ToList();
            List<StepWave> wave = wavesToPlay[0];
            wavesToPlay.Remove(wave);
            PlayWave(wave);
            yield return new WaitForSeconds(wave.Select(x => x.time).Sum());
            StartCoroutine(Play());
        }
        
    }

    private void PlayWave (List<StepWave> steps)
    {
        Wave.Create()
            .SetOrigin(Vector3.zero)
            .SetEnemyPrefab(prefabEnemy)
            .SetSteps(steps.ToArray());
    }
}
