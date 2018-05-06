using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	Text text;
	public bool countingUp;
	public float m_time;
	bool m_isRunning;
	public bool isRunning{ get { return m_isRunning;} }

	public enum TimeString{ MinuteSecondsMilli, MinuteSeconds};

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = ToReadibleTime(0);
	}

	// Update is called once per frame
	void Update () {

		if(m_isRunning)
		{
			if(countingUp)
			{

				m_time += Time.deltaTime;
			}
			else
			{
				m_time -= Time.deltaTime;
				if(m_time < 0)
				{
					m_time = 0;
					StopTimer();
				}
			}
			text.text = ToReadibleTime(m_time);

		}
	}
	public void SetTime(float time)
	{
		m_time = time;
	}
	public void SetTime(int minutes,float seconds)
	{
		SetTime(minutes*60+seconds);
	}

	public void StartTimer()
	{
		m_isRunning = true;
	}
		
	public void StopTimer()
	{

		m_isRunning = false;
	}

	public static string ToReadibleTime(float time)
	{

		return ToReadibleTime(time,TimeString.MinuteSeconds);
	}

	public static string ToReadibleTime(float time,TimeString timeType)
	{
		string returnString = "";
		switch(timeType)
		{
		case TimeString.MinuteSecondsMilli:
			returnString =getTimeSection( time/60);
			returnString += ":";
			returnString += getTimeSection(time%60);
			returnString += ":";
			returnString += getTimeSection((time%1)*100) ;
			break;

		case TimeString.MinuteSeconds:
			returnString =getTimeSection( time/60);
			returnString += ":";
			returnString += getTimeSection(time%60);
			break;
		}
		return returnString;
	}

	static string getTimeSection(float time)
	{
		time =Mathf.Floor(time);
		if(time > 9)
		{
			return time.ToString();
		}
		else
		{
			return "0"+ time.ToString();
		}
	}
}
