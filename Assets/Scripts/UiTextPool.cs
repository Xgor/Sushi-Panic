using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UiTextPool : MonoBehaviour {

	Text[] textPool;

	// Use this for initialization
	void Awake () {
		textPool = 	gameObject.GetComponentsInChildren<Text>();
		foreach(Text text in textPool)
		{
			text.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Text GetUnusedText()
	{
		foreach(Text text in textPool)
		{
			if(!text.IsActive())
			{
				text.gameObject.SetActive(true);
				return text;
			}
		}
		return null;
	}
}
