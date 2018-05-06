using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] musicList;
	AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMusic(int musicTrack)
	{
		music.Stop();
		music.clip = musicList[musicTrack];
		music.Play();
	}

	public void StopMusic(){ music.Stop(); }
}

