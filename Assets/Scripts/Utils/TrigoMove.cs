using UnityEngine;
using System.Collections;

public class TrigoMove : MonoBehaviour {

    #region Variable
    [Header("Amplitude")]
    [SerializeField]
    private float AmplitudeX;
    [SerializeField]
    private float AmplitudeY;

    [Header("Frequence")]
    [SerializeField]
    private float FrequenceX;
    [SerializeField]
    private float FrequenceY;

    private float OffsetX;
    private float OffsetY;
    #endregion

    private void Start()
    {
        OffsetX = Random.Range(0, 2 * Mathf.PI);
        OffsetY = Random.Range(0, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.up * Mathf.Sin(OffsetY + Time.time * FrequenceY) * AmplitudeY + Vector3.right * Mathf.Cos(OffsetX + Time.time * FrequenceX) * AmplitudeX;
    }
}
