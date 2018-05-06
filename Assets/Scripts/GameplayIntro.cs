using UnityEngine;
using System.Collections;

public class GameplayIntro : MonoBehaviour {
	public float speed;
	public Vector2 goalPos;
	Vector2 startPos;
	static GameManager game;
	RectTransform rectTrans;

	// Use this for initialization
	void Start () {
		rectTrans =GetComponent<RectTransform>();
		if(rectTrans != null)
		{
			startPos = rectTrans.anchoredPosition;
		}
		else
		{
			startPos = transform.position;
		}
		if(game == null)
		{
			game = (GameManager)FindObjectOfType<GameManager>();
		}
	}
	// Update is called once per frame
	void Update () {
		switch(game.gameState)
		{
		case GameManager.GameState.DayIntro:
			if(rectTrans != null)
			{
				rectTrans.anchoredPosition3D = Vector2.MoveTowards(rectTrans.anchoredPosition , goalPos ,speed*Time.deltaTime*Screen.height);
			}
			else
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos ,speed * Time.deltaTime);
			}
			break;

		case GameManager.GameState.Results:
			if(rectTrans != null)
			{ 
				rectTrans.anchoredPosition3D  = Vector2.MoveTowards(rectTrans.anchoredPosition , startPos,speed*Time.deltaTime*Screen.height);
			}
			else
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos,speed * Time.deltaTime);
			
			}
			break;
		case GameManager.GameState.Title:
			if(rectTrans != null)
			{
				rectTrans.anchoredPosition3D = Vector2.MoveTowards(rectTrans.anchoredPosition , startPos,speed*Time.deltaTime*Screen.height);
			}
			else
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos,speed * Time.deltaTime);
			}
			break;
		}

	}
}
