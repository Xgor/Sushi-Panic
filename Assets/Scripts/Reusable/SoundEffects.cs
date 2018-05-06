using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour {

	public AudioClip[] clips;
	public float minPitch;
	public float pitchVariation;
	// Use this for initialization
	void Start () {
		if(clips.Length>0)
		{
			GetComponent<AudioSource>().clip = clips[Random.Range(0,clips.Length)];
		}
		GetComponent<AudioSource>().pitch = minPitch+pitchVariation*Random.value;
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
