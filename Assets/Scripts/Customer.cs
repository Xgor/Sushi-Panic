using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	public Sprite[] hairImages;
	public Color[] hairColors;
	public Sprite[] faceImages;
	public Color[] headColors;
	public Sprite[] bodyImages;
	public Color[] bodyColors;
	public SpriteRenderer hairSprite;
	public SpriteRenderer headSprite;
	public SpriteRenderer faceSprite;
	public SpriteRenderer bodySprite;
	// Use this for initialization
	void Start () {
		NewRandomCustomer();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			NewRandomCustomer();
		}
	}

	public void NewRandomCustomer()
	{
		hairSprite.sprite = hairImages[Random.Range(0,hairImages.Length)];
		hairSprite.color = hairColors[Random.Range(0,hairColors.Length)];
		faceSprite.sprite = faceImages[Random.Range(0,faceImages.Length)];
		headSprite.color = headColors[Random.Range(0,headColors.Length)];
		bodySprite.sprite = bodyImages[Random.Range(0,bodyImages.Length)];
		bodySprite.color = bodyColors[Random.Range(0,bodyColors.Length)];

	}
}
