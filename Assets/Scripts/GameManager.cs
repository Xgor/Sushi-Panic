using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
	public enum GameState{ Title, DayIntro,Gameplay,PausedGameplay,Results};

	float currentSushiNumber; 
	Grabbable grabbedItem;
	Sushi selectedSushi;
	public Ordering[] orders;
	public Ingredient[] ingredientObjects;
	public Sushi[] sushiObjects;
	public ConveyorBelt belt;
	bool mouseOnBelt;
	byte starsGotten;

	int m_money;
	public int money
	{
		
		get{ return m_money; }
		set
		{
			m_money = value;
			moneyText.text = "¥"+value.ToString();
		}
	}

	GameState m_gameState;
	public GameState gameState{get{ return m_gameState; } }

	Level m_currentLevel;
	public Level currentLevel{ set{ m_currentLevel = value; } }

	public Text moneyText;
	public Timer timer;
	float introTimer;
	public GameObject title;
	public GameObject pausedScreen;
	public GameObject pauseButton;
	public GameObject goal;
	public GameObject countReady;
	public GameObject count3;
	public GameObject count2;
	public GameObject count1;
	public GameObject countGo;
	public GameObject results3Star;
	public GameObject results2Star;
	public GameObject results1Star;
	public Transform SushiConveyPoint;
	public Text textGoal3Star;
	public Text textGoal2Star;
	public Text textGoal1Star;
	public Text results;
	public InputField levelsEnabled;
	// Level variables
	byte currentOrderAmount = 0;
	float timeToNextOrder;

	// Ljudeffekter
	public GameObject sound_sucess;
	public GameObject sound_fail;
	public GameObject sound_pickUp;
	public GameObject sound_AddIngrediens;
	public GameObject sound_win;
	public GameObject sound_go;
	public GameObject sound_hoi;
	public GameObject sound_thank;

	public SaveFile save;
	public MusicManager music;
	Level[] levels;
	void Awake()
	{
	//	Screen.SetResolution(640, 480, true);
	}

	// Use this for initialization
	void Start () {
//		Application.targetFrameRate = 60;
		levels = FindObjectsOfType<Level>();
	//	music.SetMusic(0);
		ChangeGameState(GameState.Title);
//		ChangeGameState(GameState.Gameplay);
		UpdateAvalibleLevels(); 
		//Sätter så att man kan bara spela vissa banor


	}

	void UpdateAvalibleLevels()
	{
		foreach(Level level in levels)
		{
			if(save.levelsCompleted+1 > level.levelID)
			{
				level.IsUnlocked(true);
			}
			else
			{
				level.IsUnlocked(false);
			}
		}
	}


	public void NewOrder()
	{

		foreach(Ordering order in orders)
		{
			if(order != null && !order.orderIsActive )
			{
				if(m_currentLevel.orderNumbers.Length > 0)
				{
					order.InitNewOrder(m_currentLevel.orderNumbers[Random.Range(0,m_currentLevel.orderNumbers.Length)]);
					Instantiate(sound_hoi);
					currentOrderAmount++;
					break;
				}
			}
		}
	}
	public void StartDay()
	{
		introTimer = 3;
		count3.SetActive(true);
		count3.transform.localScale = Vector3.one;
		ChangeGameState(GameState.DayIntro);
		currentOrderAmount = 0;
		money = 0;
		timeToNextOrder = 60/m_currentLevel.ordersPerMinute;
		title.SetActive(false);
	}

	void ResetDayValues()
	{
		// Avaktivera alla beställningar
		foreach(Ordering order in orders)
		{
			if(order.orderIsActive )
			{
				// Ändra så det inte misslyckas utan bara försvinner
				order.FailedOrder();
			}
		}
		currentOrderAmount = 0;
		DropItem();
		// Avaktivera alla Sushi Tallrikar
		foreach(Sushi sushi in sushiObjects)
		{
			sushi.ResetSushi();
		}
		selectedSushi = null;
		mouseOnBelt = false; 
	}

	void EndDay()
	{
		starsGotten = 0;
		timer.StopTimer();
		if(m_currentLevel.moneyGoal == 0)
		{
			results.text = "Du fick ¥"+money.ToString();
		}
		else if(m_currentLevel.levelType == LevelType.MoneyToWin&&m_currentLevel.levelTime >= timer.m_time)
		{
	//		starsGotten
			if(m_currentLevel.goal3Star <= timer.m_time)
			{
				starsGotten = 3;
			}
			else if(m_currentLevel.goal2Star <= timer.m_time)
			{
				starsGotten = 2;
			}
			else
			{
				starsGotten = 1;
			}

			Instantiate(sound_win);
		}
		else if(m_currentLevel.levelType == LevelType.TimedChallenge&&m_currentLevel.moneyGoal <= money)
		{
			if(m_currentLevel.goal3Star <= money)
			{
				starsGotten = 3;
			}
			else if(m_currentLevel.goal2Star<= money)
			{
				starsGotten = 2;
			}
			else
			{
				starsGotten = 1;
			}
			Instantiate(sound_win);
		}

		results.text = "Grattis du klarade dagen";
		switch(starsGotten)
		{
		case 3:
			save.SaveLevelComplete(m_currentLevel.levelID,3);
			results3Star.SetActive(true);
			results2Star.SetActive(true);
			results1Star.SetActive(true);
			break;
		case 2:
			save.SaveLevelComplete(m_currentLevel.levelID,2);
			results2Star.SetActive(true);
			results1Star.SetActive(true);
			break;
		case 1:
			save.SaveLevelComplete(m_currentLevel.levelID,1);
			results1Star.SetActive(true);
			break;
		case 0:
			results.text = "Du klarade inte banan, Försök igen";
			break;
		}

		ResetDayValues();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			QuitGame();
		}

		switch(m_gameState)
		{
		case GameState.Title:
			title.SetActive(true);
			if(Input.GetMouseButtonDown(0))
			{
		//		StartDay();

			}
			break;
		case GameState.DayIntro:
			introTimer -= Time.deltaTime;
			if(introTimer < 2 && count3.activeSelf)
			{
				count3.SetActive(false);
				count2.SetActive(true);
				count2.transform.localScale = Vector3.one;
			}
			else if(introTimer < 1 && count2.activeSelf)
			{
				count2.SetActive(false);
				count1.SetActive(true);
				count1.transform.localScale = Vector3.one;
			}
			else if(introTimer < 0)
			{
				count1.SetActive(false);
				ChangeGameState(GameState.Gameplay);
			}
			break;
		case GameState.Gameplay:
			GameplayUpdate();
			break;
		case GameState.Results:
			if(Input.GetMouseButtonDown(0))
			{
				ChangeGameState(GameState.Title);
			}
			break;
		}
	}

	void UpdateGrabbedItem()
	{
		Vector3 pos;
		if(Application.isMobilePlatform)
		{
			pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		}
		else
		{
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		pos.z = 0;
		grabbedItem.transform.position = pos;
		if(	Input.GetMouseButtonUp(0))
		{

			Ingredient grabbedIngredient = grabbedItem.GetComponent<Ingredient>();
			Sushi grabbedSushi = grabbedItem.GetComponent<Sushi>();

			// Om spelaren drar en ingrediens på en sushibit så lägg ihop värderna och ge ett nytt värde
			if(selectedSushi != null && grabbedIngredient != null)
			{
				selectedSushi.sushiNumber = grabbedIngredient.AddIngredient(selectedSushi.sushiNumber);
				grabbedIngredient.NewValue();
				selectedSushi.GetComponent<Squasher>().m_xSquish = 0.5f;
				selectedSushi.AddIngredientImage(grabbedIngredient);

				Instantiate(sound_AddIngrediens);
				 // Förlorar pengar för varje använd ingrediens
				/*
				money -= 5;
				*/
			}
			// Om spelaren drar en sushibit på löpband 
			if(mouseOnBelt && grabbedSushi != null)
			{
				belt.ActivateAnimation();
				bool failedOrder= true;
				grabbedSushi.transform.position = SushiConveyPoint.transform.position;
				//Kollar om spelaren skicade in en av de korrekta beställningarna
				foreach(Ordering order in orders)
				{
					if(order != null && order.orderIsActive)
					{ 
						// Om beställningen är lyckad
						if(order.orderNumber == grabbedSushi.sushiNumber)
						{
						//	grabbedSushi.GetComponent<Grabbable>().Release();
							failedOrder = false;
							order.SucessfulOrder();
							pos.z = 0; 

							Instantiate(sound_sucess,SushiConveyPoint.position,Quaternion.identity);
							SucessfulOrder();
							break;
						}
					} 
				}
				// Om det inte var en av de korrekta beställningarna
				if(failedOrder)
				{
					Instantiate(sound_fail);
					grabbedSushi.ResetSushi();
				}

				grabbedSushi.SetOnConveyor();
				grabbedItem = null;
			}
			// Släpper saken och sätter tillbaka den till originalpositionen om den inte är placerad på bältet
			else
			{
				DropItem();
			}

			// Nollställer om man har fingret över något 
			if(Application.isMobilePlatform) 
			{
				selectedSushi = null;
				mouseOnBelt = false; 
			}
		}
	}

	void SucessfulOrder()
	{
		timeToNextOrder += 1;

		currentOrderAmount--;
		Instantiate(sound_thank);
		// Gör så man klarar banan
		if(m_currentLevel.levelType == LevelType.MoneyToWin && m_currentLevel.moneyGoal <= m_money)
		{
			EndDay();
			ChangeGameState(GameState.Results);
		}
	}

	void GameplayUpdate()
	{

		if(grabbedItem != null)
		{
			UpdateGrabbedItem();
		}

		timeToNextOrder -= Time.deltaTime;
		if(timeToNextOrder<0)
		{
			timeToNextOrder += 60/m_currentLevel.ordersPerMinute;
			if(currentOrderAmount < m_currentLevel.maxAmountOfOrders)
			{

				NewOrder();
			}

		}
	
		// Kollar om det är inga aktiva beställningar och startar en om det inte är en
		bool noOrders = true;
		foreach(Ordering order in orders)
		{
			if(order.orderIsActive )
			{
				noOrders = false;
				break;
			}
		}

		if(noOrders)
		{
		//	NewOrder();
		}


		// Om tiden är ute på tidsutmaningar
		if(m_currentLevel.levelType == LevelType.TimedChallenge && !timer.isRunning)
		{
			EndDay();
			ChangeGameState(GameState.Results);
		}
	}


	public void SetGrabbedItem(Grabbable item)
	{
		if(grabbedItem != null)
		{
			DropItem();
		}
		grabbedItem = item;
		Instantiate(sound_pickUp);
	}
		

	public void PointerExitSushi()
	{

		selectedSushi = null;

	}

	public void PointerExitBelt()
	{

		mouseOnBelt = false; 

	}

	void DropItem()
	{
		grabbedItem.ResetPosition();
		grabbedItem = null;
	}

	public void LowerActiveOrderAmount(){ currentOrderAmount--; }
	public void PressPauseButton(){ ChangeGameState(GameState.PausedGameplay); }
	public void Unpause() {		ChangeGameState(GameState.Gameplay); }
	public void QuitGame(){ Application.Quit(); }
	public void PointerEnterBelt(){ mouseOnBelt = true; }
	public void PointerEnterSushi(Sushi sushi){ selectedSushi = sushi; }


	public void ButtonBackToMenu()
	{
		ChangeGameState(GameState.Title);
		ResetDayValues();
	}



	public void ChangeGameState(GameState newState)
	{
		if(m_gameState != newState)
		{
			// Kör koden när den slutar med den staten
			switch(m_gameState)
			{
			case GameState.Title:
				timer.StopTimer();
				timer.m_time = 0;
		//		music.StopMusic();
				break;
			case GameState.DayIntro:
				goal.gameObject.SetActive(false);
				results.gameObject.SetActive(false);
				if(m_currentLevel.levelType == LevelType.TimedChallenge)
				{
					timer.countingUp = false;
					timer.SetTime(m_currentLevel.levelTime);
					timer.StartTimer();

				}
				else
				{
					timer.countingUp = true;
					timer.SetTime(0);
					timer.StartTimer();
				}
				NewOrder();
				break;
			case GameState.Gameplay:
				foreach(Sushi sushi in sushiObjects)
				{
					sushi.gameObject.SetActive(false);
				}

				break;
			case GameState.Results:
				results3Star.SetActive(false);
				results2Star.SetActive(false);
				results1Star.SetActive(false);

				results.gameObject.SetActive(false);

				foreach(SoundEffects sound in FindObjectsOfType<SoundEffects>())
				{
					Destroy(sound.gameObject);
				}

				break;
			case GameState.PausedGameplay:
				pausedScreen.SetActive(false);
				timer.StartTimer();
				foreach(Ordering order in orders)
				{
					order.gameObject.SetActive(true);
				}

				for(int i = 0;sushiObjects.Length> i;i++)
				{
					if(i < m_currentLevel.amountOfPlates)
					{
						sushiObjects[i].gameObject.SetActive(true);
					}
					else
					{
						sushiObjects[i].gameObject.SetActive(false);
					}
				}

				foreach(Ingredient ingredient in ingredientObjects)
				{
					if(ingredient.modifierNumber != 0)
					{
						ingredient.SetActive(true);
					}
				}
				pauseButton.SetActive(true);
				break;
			}


			m_gameState = newState;

			// Kör koden när den börjar med den nya staten
			switch(newState)
			{
			case GameState.Title:
				print(newState.ToString());
				UpdateAvalibleLevels();
				foreach(Ingredient ingredient in ingredientObjects)
				{
					ingredient.modifierNumber = 0;
				}
		//		music.SetMusic(0);
				break;
			case GameState.DayIntro:
//				goal.gameObject.SetActive(true);
				foreach(Sushi sushi in sushiObjects)
				{
					sushi.gameObject.SetActive(true);
				}
				break;
			case GameState.Gameplay:
				
				break;
			case GameState.Results:
				
				results.gameObject.SetActive(true);
				break;
			case GameState.PausedGameplay:
				pausedScreen.SetActive(true);
				timer.StopTimer();
				foreach(Sushi sushi in sushiObjects)
				{
					sushi.gameObject.SetActive(false);
				}
				foreach(Ingredient ingredient in ingredientObjects)
				{
					ingredient.SetActive(false);
				}
				foreach(Ordering order in orders)
				{
					order.gameObject.SetActive(false);
				}
				pauseButton.SetActive(false);
				break;
			}
		}
	}

	public bool IsNumberOrdered(float value)
	{
		foreach(Ordering order in orders)
		{
			if(order != null && order.orderIsActive)
			{

				if(order.orderNumber == value)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void ChangeAvalibleLevels()
	{
		save.SetAmountOfCompletedLevels(short.Parse(levelsEnabled.text));
		UpdateAvalibleLevels();
	}
}