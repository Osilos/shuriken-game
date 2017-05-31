using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct StepWave
{
    public Vector3 position;
    public float time;

    public StepWave(Vector3 position, float time)
    {
        this.position = position;
        this.time = time;
    }
}
