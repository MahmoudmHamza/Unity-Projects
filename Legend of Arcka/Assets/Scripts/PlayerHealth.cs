using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] private int startingHealth = 100;
	[SerializeField] private float timeSinceLastHit = 2f;
	[SerializeField] private Slider healthBar;

	private int currentHealth;
	private Animator anim;
	private CharacterController characterController;
	private AudioSource audio;
	private ParticleSystem blood;
	private float timer = 0f;

	public int CurrentHealth {       // public getter and setter for current HP.
		get { return currentHealth; }
		set {
			if (value < 0) {
				currentHealth = 0;
			} else {
				currentHealth = value;
			}
		}
	}

	void Awake()
	{
		Assert.IsNotNull (healthBar);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		characterController = GetComponent<CharacterController> ();
		audio = GetComponent<AudioSource> ();
		blood = GetComponentInChildren<ParticleSystem> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			
			if (other.tag == "Weapon") {
				takeHit ();
				timer = 0;
			}
		}
	}

	void takeHit()
	{
		if (currentHealth > 0) {
			GameManager.instance.PlayerHit (currentHealth);
			anim.Play ("Hurt");
			currentHealth -= 10;
			healthBar.value = currentHealth;
			audio.PlayOneShot (audio.clip);
			blood.Play ();
		}
		if (currentHealth <= 0) {
			killPlayer ();
		}
	}

	void killPlayer()
	{
		GameManager.instance.PlayerHit (currentHealth);
		characterController.enabled = false;
		anim.SetTrigger ("HeroDie");
		audio.PlayOneShot (audio.clip);
		blood.Play ();
	}

	public void healthPowerUp()
	{
		if (currentHealth <= 70) {
			currentHealth += 30;
		}
		else if (currentHealth < startingHealth) {
			currentHealth = startingHealth;
		}
		healthBar.value = currentHealth;
	}
}
