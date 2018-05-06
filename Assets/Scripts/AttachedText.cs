using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttachedText : MonoBehaviour {
	
	static UiTextPool textPool;

	string m_textString;
	Text m_text;
	public Vector3 postionOffset;
	public int fontSize;
	public bool shadow;
	Text m_shadowText;

	// Use this for initialization
	void Awake () {
		if(textPool == null)
		{
			textPool = (UiTextPool)FindObjectOfType<UiTextPool>();
		}

		if(shadow)
		{
			m_shadowText = textPool.GetUnusedText();
		}
		m_text = textPool.GetUnusedText();


		if(fontSize> 0)
		{
			m_text.fontSize= fontSize;
		}

		if(shadow)
		{
			m_shadowText.color = Color.white;
			m_shadowText.fontSize = fontSize;
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_text.transform.position = transform.position+postionOffset; 
		if(m_shadowText != null)
		{
			m_shadowText.transform.position = transform.position+postionOffset+Vector3.forward*0.03f+Vector3.right*0.05f+Vector3.down*0.05f;
//			m_shadowText2.transform.position = transform.position+postionOffset+Vector3.forward*0.03f+Vector3.left*0.03f;
		}

	}
	 
	public Text GetText()
	{
		return m_text;
	}

	public void SetTextString(string text)
	{
		m_textString = text;
		m_text.text = text;
		if(shadow)
		{
			m_shadowText.text = text;
		}
	}

	public void SetTextActive(bool value)
	{
		m_text.enabled = false;
		if(shadow)
		{
			m_shadowText.enabled = true;
		}
	}
}
