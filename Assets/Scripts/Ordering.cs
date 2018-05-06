using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Ordering : MonoBehaviour {


	public float appearSpeed = 300;
	int _orderNumber;
	public int orderNumber
	{
		get { return _orderNumber; }
	}

	bool m_orderIsActive;
	public bool orderIsActive
	{
		get { return m_orderIsActive; }
	}

	public float timePerOrder;

//	public Text orderNumberText;
	public AttachedText orderNumberText;
	public Vector3 activeOrderOffset;

	public Customer customer;

	float orderTimeLeft;
	static GameManager game;

	Vector3 startPos;
	Image barImg;

	// Gammal kod för unity UI
//	public RectTransform TimeBar;
//	float barSize;

	// Use this for initialization
	void Start () {
		startPos = transform.position;

		if(game == null)
		{
			game = (GameManager)FindObjectOfType<GameManager>();
		}

		// Gammal kod för unity UI
//		barSize = TimeBar.rect.width;
//		barImg = TimeBar.GetComponent<Image>();
	}


	public void InitNewOrder(float value)
	{
		orderTimeLeft = 1;
		_orderNumber = (int)value;
		m_orderIsActive = true;
		orderNumberText.SetTextString( orderNumber.ToString());
		customer.NewRandomCustomer();
	}

	public void InitNewOrder(int min, int max)
	{
		InitNewOrder(Random.Range(min,max));
	}

	// Update is called once per frame
	void Update () {
		orderTimeLeft -= Time.deltaTime/ timePerOrder;



		if(m_orderIsActive)
		{
			if(orderTimeLeft < 0)
			{
				FailedOrder();
			}
			float distToGoal = Vector3.Distance(transform.position, startPos+activeOrderOffset);
			transform.position = Vector3.MoveTowards(transform.position,startPos+activeOrderOffset,appearSpeed*Time.deltaTime*distToGoal);
			/*
			Vector2 newBarPos =TimeBar.GetComponent<RectTransform>().sizeDelta;
			newBarPos.x = orderTimeLeft* barSize;
			barImg.rectTransform.sizeDelta = newBarPos; 

			if(orderTimeLeft <0.25f)
			{
				barImg.color = Color.red;
			}
			else if(orderTimeLeft <0.5f)
			{
				barImg.color = Color.yellow;
			}
			else
			{
				barImg.color = Color.green;
			}
			*/
		}
		else
		{
			float distToGoal = Vector3.Distance(transform.position, startPos);
			transform.position = Vector3.MoveTowards(transform.position,startPos,appearSpeed*Time.deltaTime*distToGoal);

		}

	}


	public void FailedOrder()
	{
		m_orderIsActive = false;
		game.LowerActiveOrderAmount();
	}

	public void SucessfulOrder()
	{
		m_orderIsActive = false;
		game.money += 100;
	}

	void OnDisable() 
	{
		if(orderNumberText.GetText() != null)
			orderNumberText.SetTextActive(false);
	}

	void OnEnable()
	{
		if(orderNumberText.GetText() != null)
			orderNumberText.SetTextActive(true);
	}
}
