using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	private static readonly float zLimit = 300f;
	private static readonly float zRespawn = -300f;

	[SerializeField]
	private float speed;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * speed * Time.deltaTime);
		if (transform.position.z > zLimit)
			transform.position = new Vector3(transform.position.x, transform.position.y, zRespawn);
	}
}
