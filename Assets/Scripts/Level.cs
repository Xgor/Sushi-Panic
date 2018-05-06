using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum LevelType { TimedChallenge, MoneyToWin};

public class Level : MonoBehaviour {



	public short levelID;
	public string[] ingredients;
	public Sprite[] ingredientSprites;
	public float[] orderNumbers;
	public byte maxAmountOfOrders;
	public byte amountOfPlates;



	// Målet för att klara banan 
	// Timed = svårare hur mycket pengar man samlar in
	// Money = svårare hur lite tid man har för att klara det (-1 oändligt med tid)
	public LevelType levelType;
	public int goal1Star;
	public int goal2Star;
	public int goal3Star;

	public float ordersPerMinute;
	public float timePerOrder;

	// Används bara av en level typ (moneyGoal för Money och levelTime för Timed)
	public int moneyGoal;
	public float levelTime;

	public void StartLevel(GameManager game)
	{
		
		foreach(Ordering order in game.orders)
		{
			order.timePerOrder = timePerOrder;
		} 

		for(int i = 0;game.ingredientObjects.Length> i;i++)
		{
			if(i < ingredients.Length)
			{
				game.ingredientObjects[i].SetValue(ingredients[i]);
				game.ingredientObjects[i].SetActive(true);
				if(i < ingredientSprites.Length)
				{
					game.ingredientObjects[i].SetSprite(ingredientSprites[i]);
				}
			}
			else
			{
				game.ingredientObjects[i].SetActive(false);
			}
		}

		for(int i = 0;game.sushiObjects.Length> i;i++)
		{
			if(i < amountOfPlates)
			{
				game.sushiObjects[i].gameObject.SetActive(true);
			}
			else
			{
				game.sushiObjects[i].gameObject.SetActive(false);
			}
		}
		game.currentLevel = this;

		switch(levelType)
		{

		case LevelType.MoneyToWin:
			game.textGoal3Star.text = Timer.ToReadibleTime(goal3Star);
			game.textGoal2Star.text = Timer.ToReadibleTime(goal2Star);
			game.textGoal1Star.text = Timer.ToReadibleTime(goal1Star);
			break;
		case LevelType.TimedChallenge:
			game.textGoal3Star.text = "¥"+goal3Star;
			game.textGoal2Star.text = "¥"+goal2Star;
			game.textGoal1Star.text = "¥"+goal1Star;
			break;
		}
//		game.textGoal3Star

/*
		game.levelTime = levelTime;
		game.amountOfPlates = amountOfPlates;
		game.orderNumbers = orderNumbers;

		// Överför alla värden till gameManager
		game.moneyGoal = moneyGoal;
		game.maxAmountOfOrders = maxAmountOfOrders;
		game.ordersPerSecond = 60/ordersPerMinute;
		game.currentlevelID = levelID;
		*/
		// Startar igång en spelomgång
		game.StartDay();
	}  

	public void IsUnlocked(bool unlocked)
	{
		GetComponent<Button>().interactable= unlocked;
	}
}
