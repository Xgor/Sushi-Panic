using UnityEngine;
using System.Collections;

public class ScaleUp : MonoBehaviour {

	public float scaleSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
	}
}
