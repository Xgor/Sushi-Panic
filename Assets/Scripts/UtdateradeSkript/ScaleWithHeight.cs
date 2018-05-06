using UnityEngine;
using System.Collections;

public class ScaleWithHeight : MonoBehaviour {

	RectTransform rectTrans;

	// Use this for initialization
	void Start () {
		rectTrans = GetComponent<RectTransform>();


	}

	// Update is called once per frame
	void Update () {
		Vector2 newSize = rectTrans.localScale;

		newSize.x = Screen.height/600.0f;
		newSize.y = Screen.height/600.0f;
		transform.localScale = newSize;
	}
}
