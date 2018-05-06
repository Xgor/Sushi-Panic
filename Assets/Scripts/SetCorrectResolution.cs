using UnityEngine;
using System.Collections;

public class SetCorrectResolution : MonoBehaviour {

	RectTransform rectTrans;

	// Use this for initialization
	void Start () {
		rectTrans = GetComponent<RectTransform>();


	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newSize;

		//	newRect = rectTrans.rect;
		newSize.x = Screen.width;
		newSize.y = Screen.height;
	//	Camera.main.pixelRect = Screen.width;
	//	Camera.main.pixelRect = Screen.height;
		rectTrans.sizeDelta = newSize;
	}
}
 