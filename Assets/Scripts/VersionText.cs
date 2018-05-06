using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;

public class VersionText : MonoBehaviour {

	Text text;
	// Use this for initialization
	void Start () {
		
		text=	GetComponent<Text>();
//		PlayerSettings.bundleIdentifier = 
	}
	
	// Update is called once per frame
	void Update () {

		text.text = Application.version;
	}
}
