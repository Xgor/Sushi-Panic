using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ingredient :  MonoBehaviour {

	public enum Arithmetic { Addition,Subtraction, Multiplication, 
				Division,Exponentiation, Round, SquareRoot,Absolute
	};

//	public Text modifierText;
	public AttachedText modifierText;
	public SpriteRenderer[] heapIngredients;
	Squasher squasher;
	public bool staticIngredient;
	public Arithmetic modifierType;
	public int modifierNumber;
	// Use this for initialization
	void Start () { 
		squasher = GetComponent<Squasher>();
	//	modifierText = GetComponent<Text>();

		NewValue();

	}
	
	// Update is called once per frame
	void Update () {
		if(modifierText == null)
		{
			modifierText = GetComponent<AttachedText>();
		//	modifierText = attachedText.GetText();
			UpdateText();
		}
	}

	public void SetSprite(Sprite sprite)
	{
		GetComponent<SpriteRenderer>().sprite = sprite;
		foreach(SpriteRenderer heapIngredient in heapIngredients)
		{
			heapIngredient.sprite= sprite;
		}
	}

	public void SetActive(bool value)
	{
		gameObject.SetActive(value);
		foreach(SpriteRenderer heapIngredient in heapIngredients)
		{
			heapIngredient.enabled = value;
		}
	}

	// Ger ett nytt värde till ingrediensen
	public void NewValue()
	{
		if(!staticIngredient)
		{
			switch(Random.Range(1,8))
			{

			// Addition
			case 1:
				modifierType = Arithmetic.Addition;
				modifierNumber = Random.Range(1,5);

				break;

				// Subtraction
			case 2:
				modifierType = Arithmetic.Subtraction;
				modifierNumber = Random.Range(1,5);
				break;


				// Multiplication
			case 3:
				modifierType = Arithmetic.Multiplication;
				modifierNumber = Random.Range(2,3);
				break;


			default:
				modifierType = Arithmetic.Addition;
				modifierNumber = Random.Range(1,5);
				break;
			}


		}
		squasher.m_xSquish = 0.5f;
		if(modifierText != null)
		{
			UpdateText();
		}
	}

	public void SetValue(string value)
	{
		switch(value[0])
		{

		// Addition
		case '+':
			modifierType = Arithmetic.Addition;
			modifierNumber = int.Parse(	value.Substring(1));
			break;

			// Subtraction
		case '-':
			modifierType = Arithmetic.Subtraction;
			modifierNumber = int.Parse(	value.Substring(1));
			break;

			// Multiplication
		case '*':
			modifierType = Arithmetic.Multiplication;
			modifierNumber = int.Parse(	value.Substring(1));
			break;

		case 'x':
			modifierType = Arithmetic.Multiplication;
			modifierNumber = int.Parse(	value.Substring(1));
			break;
			// Division
		case '/':
			modifierType = Arithmetic.Division;
			modifierNumber = int.Parse(	value.Substring(1));
			break;

			// Exponentiation
		case '^':
			modifierType = Arithmetic.Exponentiation;
			modifierNumber = int.Parse(	value.Substring(1));
			break;

		case 'R':
			modifierType = Arithmetic.Round;
			break;

		case 'S':
			modifierType = Arithmetic.SquareRoot;
			break;

		case 'A':
			modifierType = Arithmetic.Absolute;
			break;

			// Addition
		default:
			modifierNumber = int.Parse(	value);
			break;

		}

		if(modifierText != null)
		{
			UpdateText();
		}
	}

	// Updates the text so it shows the correct modifier and number
	void UpdateText()
	{
		switch(modifierType)
		{
		// Addition
		case Arithmetic.Addition:
			modifierText.SetTextString("+" +  modifierNumber.ToString());
			break;

			// Subtraction
		case Arithmetic.Subtraction:
			modifierText.SetTextString("-" + modifierNumber.ToString());
			break;

			// Multiplication
		case Arithmetic.Multiplication:
			modifierText.SetTextString("x" + modifierNumber.ToString());
			break;

		case Arithmetic.Division:
			modifierText.SetTextString("÷" + modifierNumber.ToString());
			break;

		case Arithmetic.Exponentiation:
			if(modifierNumber == 2)
			{
				modifierText.SetTextString( "x²");
			}
			else if(modifierNumber == 3)
			{
				modifierText.SetTextString( "x³");
			}
			break;

		case Arithmetic.Round:
			modifierText.SetTextString("round");
			break;

		case Arithmetic.SquareRoot:
			modifierText.SetTextString("√");
			break;

		case Arithmetic.Absolute:
			modifierText.SetTextString("abs");
			break;
		}
	}

	// Slår ihopp en ingrediens värde med en sushibit
	public int AddIngredient(int currentNumber)
	{
		int returnNumber = currentNumber;
		switch(modifierType)
		{ 
		// Addition
		case Arithmetic.Addition:
			returnNumber += modifierNumber;
			break;

			// Subtraction
		case Arithmetic.Subtraction:
			returnNumber -= modifierNumber;
			break;

			// Multiplication
		case Arithmetic.Multiplication:
			returnNumber *= modifierNumber;
			break;

		case Arithmetic.Division:
			if(returnNumber != 0)
			{
				returnNumber /= modifierNumber;
			}
			break;

		case Arithmetic.Exponentiation:
			returnNumber = (int)Mathf.Pow((float)returnNumber,(float)modifierNumber);
			break;

		case Arithmetic.Round:
	//		returnNumber = Mathf.Round( returnNumber);
			break;

		case Arithmetic.SquareRoot:
	//		returnNumber = Mathf.Sqrt( returnNumber);
			break;

		case Arithmetic.Absolute:
			returnNumber = Mathf.Abs( returnNumber);
			break;
		}

		return returnNumber;
	}

	void OnDisable() 
	{

		if(modifierText != null)
			modifierText.SetTextActive(false);
	}

	void OnEnable()
	{
		if(modifierText != null)
			modifierText.SetTextActive(true);
	}
}
