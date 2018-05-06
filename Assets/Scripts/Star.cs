using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public float animTime;
	float timeLeft;

	// Use this for initialization
	void OnEnable() {
		timeLeft = animTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft >0)
		{
			timeLeft -= Time.deltaTime;
			transform.rotation = Quaternion.Euler(0,0,timeLeft *100.0f);
			transform.localScale = (timeLeft+1)*Vector3.one;
		}
	}
}
