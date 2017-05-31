using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct StepWave
{
    public float timeBeforeSpawn;
    public Vector3 position;

    public StepWave(Vector3 position, float time)
    {
        this.position = position;
        this.timeBeforeSpawn = time;
    }
}
