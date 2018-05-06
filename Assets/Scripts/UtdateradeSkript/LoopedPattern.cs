using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoopedPattern : MonoBehaviour {
	public Vector2 speed;
	public Vector2 patternSize;
	//public Vector2 t;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Time.deltaTime*speed);
		Vector2 tempPos = transform.localPosition;
	//	print( transform.localPosition.y);
		if(patternSize.x < transform.localPosition.x)
		{
			tempPos.x -= patternSize.x;
		}
		if(patternSize.y < transform.localPosition.y)
		{
			tempPos.y -= patternSize.y;
		}
		transform.localPosition = tempPos;
	}
}
