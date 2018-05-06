using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Grabbable : MonoBehaviour {
	Vector2 startPos;
	MaskableGraphic uiElement;
	static GameManager game;
	bool grabbed;
	Squasher squasher;
	SinRotate rotator;

	// Use this for initialization
	void Start () {
		uiElement = GetComponent<MaskableGraphic>();
		startPos = transform.localPosition;
		if(game == null)
		{
			game = (GameManager)FindObjectOfType<GameManager>();
		}
		squasher = GetComponent<Squasher>();
		rotator = GetComponent<SinRotate>();
		rotator.enabled = false;
	}


	// Update is called once per frame
	void Update () {
		if(grabbed)
		{
			squasher.m_xSquish = 1 + Mathf.Clamp( Mathf.Abs( Input.GetAxis("Mouse X"))/2,0.7f,1) -Mathf.Clamp(Mathf.Abs(Input.GetAxis("Mouse Y"))/2,0.7f,1);

		}
	}

	void OnMouseDown()
	{
		if(game.gameState == GameManager.GameState.Gameplay)
		{
			Grab();
		}
	
	}

	public void Grab()
	{
		if(Input.touchCount < 2)
		{
			foreach(Collider2D collider in GetComponents<Collider2D>())
			{
				collider.enabled = false;
			}

			game.SetGrabbedItem(this);
			grabbed = true;
			rotator.enabled = true;
			if(uiElement != null)
			{
				uiElement.raycastTarget= false;
			}
		}
	}

	public void Release()
	{
		grabbed = false;
	//	squasher.m_xSquish = 1;
	}

	public void ResetPosition()
	{
		foreach(Collider2D collider in GetComponents<Collider2D>())
		{
			collider.enabled = true;
		}
		transform.localPosition = startPos;
		grabbed = false;
		rotator.enabled = false;
		transform.rotation = Quaternion.identity;
		if(uiElement != null)
		{
			uiElement.raycastTarget= true;
		}
	}
}
