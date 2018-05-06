using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveFile : MonoBehaviour {
	public short levelAmount;

	SaveData levelData;

	public short levelsCompleted
	{
		get
		{
			short completedCount = 0;
			for(int i = 0; i+1 < levelAmount;i++)
			{
				if(levelData.LevelCompleted[i] >0)
					completedCount++;
			}
			return completedCount;
		}
	}

	// Use this for initialization
	void Awake () {
		levelData = new SaveData();
		levelData.LevelCompleted = new byte[levelAmount];
		Load();
		DontDestroyOnLoad(gameObject);
	}


	public void SetAmountOfCompletedLevels(short completedLevelAmount)
	{
		for(int i = 0; i+1 < levelAmount;i++)
		{
			if(completedLevelAmount > i)
				levelData.LevelCompleted[i] = 1;
			else
				levelData.LevelCompleted[i] = 0;
		}
		Save();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.T))
		{
			SaveLevelComplete(0,1);
		}
		if(Input.GetKeyDown(KeyCode.C))
		{
			Clear();
		}
	}
	
	void OnApplicationQuit()
	{
		Save();
	}

	public void SaveLevelComplete(int level,byte stars)
	{
		if(levelData.LevelCompleted[level] < stars)
		{
			levelData.LevelCompleted[level] = stars;
			Save();
		}
	}

	public byte LevelBeaten(int level)
	{
		return levelData.LevelCompleted[level];
	}


	void Load()
	{

		if(File.Exists(Application.persistentDataPath + "/saveFile.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/saveFile.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
			levelData = data;
		}
		else
		{

			Clear();
		}
	}

	void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open (Application.persistentDataPath + "/saveFile.dat", FileMode.Create);
		bf.Serialize(file,levelData);
		file.Close();
	}

	void Clear() 
	{
		BinaryFormatter bf = new BinaryFormatter();
		levelData.LevelCompleted = new byte[levelAmount];
		for(int i= 0; levelAmount > i+1; i++)
		{
			levelData.LevelCompleted[i] = 0;
		}
		FileStream file = File.Open (Application.persistentDataPath + "/saveFile.dat", FileMode.Create);
		
		bf.Serialize(file,levelData);
		file.Close();
	}

}

[Serializable]
class SaveData 
{
	public byte[] LevelCompleted;
}