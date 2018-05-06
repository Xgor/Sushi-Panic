using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {
	static GameManager game;
	Animator animator;
	float animatedTimeLeft;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		if(game == null)
		{
			game = (GameManager)FindObjectOfType<GameManager>();
		}
	}

	void Update()
	{
		if(animatedTimeLeft> 0)
		{
			animatedTimeLeft -= Time.deltaTime; 
		}
		else
		{
			DisactivateAnimation();
		} 
	}
	
	void OnMouseEnter()
	{
		game.PointerEnterBelt();
	}

	void OnMouseExit()
	{
		game.PointerExitBelt();
	}

	public void ActivateAnimation()
	{
		animatedTimeLeft++;
		animator.SetBool("Animating",true);
		audioSource.Play();

	}
	public void DisactivateAnimation()
	{
		animator.SetBool("Animating",false);
		audioSource.Stop();
	}
}
