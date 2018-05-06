using UnityEngine;
using System.Collections;

public class SinMove : MonoBehaviour {

	public Vector3 power;
	public float speed;
	Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position =startPos+ Mathf.Sin(speed*Time.time) *power;
	}
}
