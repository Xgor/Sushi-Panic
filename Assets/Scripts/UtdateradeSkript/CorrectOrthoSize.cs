using UnityEngine;
using System.Collections;

public class CorrectOrthoSize : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = Screen.height/2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
