using UnityEngine;
using System.Collections;

public class Squasher : MonoBehaviour {

	public float m_unsqishSpeed = 1;
	Vector2 tempVector;

	public float m_xSquish = 1f;
	public float xSquish 
	{
		get 
		{ 
			return m_xSquish;
		}
		set 
		{
			m_xSquish = xSquish % 2; 
		}
	}
	public float ySquish 
	{
		get 
		{ 
			return 2- m_xSquish;
		}
		set 
		{
			m_xSquish = 2-(ySquish % 2); 
		}
	}

	
	// Update is called once per frame
	void Update () {
		m_xSquish=Mathf.MoveTowards(m_xSquish,1.0f,Time.deltaTime*m_unsqishSpeed);

		tempVector.x = m_xSquish;
		tempVector.y = 2-m_xSquish;

		transform.localScale = tempVector;

	//	m_xSquish = (1+ (Mathf.Sin((float)Time.time)) *0.8f) ;

		if(Input.GetKeyDown(KeyCode.Space))
		{
			print("Huh");

			m_xSquish = 0.3f;
		}
	}
}
