using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sushi : MonoBehaviour {
	/// <summary>
	/// Gammal Kod för unity UI istället för sprites
	/// </summary>
	//public Image[] ingredientImages;
	public SpriteRenderer[] ingredientImages;
	int _sushiNumber = 0;
	public int sushiNumber
	{
		get
		{
			return _sushiNumber;
		}
		set
		{
			_sushiNumber = value;
			sushiText.SetTextString( sushiNumber.ToString());
			if(game.IsNumberOrdered(_sushiNumber))
			{
				sushiText.GetText().color = Color.red;
			}
			else
			{
				sushiText.GetText().color = Color.black;
			}
		}
	}
	public AttachedText sushiText;
	static GameManager game;
	bool onConveyor = false;
	public float conveyorSpeed;
	// Use this for initialization
	void Start () {

		sushiText = GetComponent<AttachedText>();
		sushiText.SetTextString ( _sushiNumber.ToString());

		if(game == null)
		{
			game = (GameManager)FindObjectOfType<GameManager>();
		}



	}
	
	// Update is called once per frame
	void Update () {
		if(onConveyor)
		{
			transform.position += Vector3.up*0.60f*conveyorSpeed*Time.deltaTime+Vector3.right*conveyorSpeed*Time.deltaTime;
	//		transform.Translate((Vector2.up*Time.deltaTime+Vector2.left*1.7f*Time.deltaTime)*10 );
			if(transform.position.y > 10)
			{
				GetComponent<Grabbable>().ResetPosition();
				onConveyor =false;
				ResetSushi();
			}
		}
	}


	void OnMouseEnter()
	{
		game.PointerEnterSushi(this);
	}

	void OnMouseExit()
	{
		game.PointerExitSushi();
	}
	public void SetOnConveyor()
	{
		onConveyor = true;
		GetComponent<SinRotate>().enabled = false;
	}
	public bool AddIngredientImage(Ingredient ingredient)
	{
		foreach(SpriteRenderer img in ingredientImages)
		{
			if(img.sprite == null)
			{
				img.sprite = ingredient.GetComponent<SpriteRenderer>().sprite;
				img.enabled = true;
				return true;
			}
		}

		ingredientImages[ingredientImages.Length-1].sprite = ingredient.GetComponent<SpriteRenderer>().sprite;
		return false;
	}


	public void ResetSushi()
	{
		sushiNumber = 0;
		foreach(SpriteRenderer ingredient in ingredientImages)
		{
			ingredient.sprite = null;
			ingredient.enabled = false;
		}
	}

	public bool IsOnConveyor()
	{
		return onConveyor;
	}


	void OnDisable() 
	{
		
		if(sushiText != null)
			sushiText.SetTextActive(false);
	}

	void OnEnable()
	{
		if(sushiText != null)
			sushiText.SetTextActive(true);
	}
}
