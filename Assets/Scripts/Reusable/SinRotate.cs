using UnityEngine;
using System.Collections;

public class SinRotate : MonoBehaviour {

	public float power;
	public float speed;

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(0,0,Mathf.Sin(speed*Time.time)*power);
	}
}
