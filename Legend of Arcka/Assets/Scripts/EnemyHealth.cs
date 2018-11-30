using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	[SerializeField] private int startingHealth = 20;
	[SerializeField] private float timeSinceLastHit = 0.5f;
	[SerializeField] private float disappearSpeed = 2f;

	private AudioSource audio;
	private NavMeshAgent nav;
	private CapsuleCollider capsuleCollider;
	private Rigidbody rigidBody;
	private Animator anim;
	private ParticleSystem blood;
	private float timer = 0f;
	private bool isAlive;
	private bool enemyDisappeared = false;
	private int currentHealth;

	public bool IsAlive {
		get {return isAlive;}
	}

	// Use this for initialization
	void Start () {
		GameManager.instance.RegisterEnemy (this);
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		rigidBody = GetComponent<Rigidbody> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		blood = GetComponentInChildren<ParticleSystem> ();
		isAlive = true;
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (enemyDisappeared) {
			transform.Translate (-Vector3.up * disappearSpeed * Time.deltaTime);
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			if (other.tag == "PlayerWeapon") {
				takeHit ();
				timer = 0f;
			}
		}
	}

	void takeHit()
	{
		if (currentHealth > 0) {
			audio.PlayOneShot (audio.clip);
			anim.Play ("Hurt");
			currentHealth -= 10;
			blood.Play ();
		}
		if (currentHealth <= 0) {
			isAlive = false;
			killEnemy ();
		}
	}
	void killEnemy()
	{
		GameManager.instance.KilledEnemy (this);
		capsuleCollider.enabled = false;
		nav.enabled = false;
		rigidBody.isKinematic = true; //is kinematic means ignoring the physics.
		anim.SetTrigger ("EnemyDie");
		audio.PlayOneShot (audio.clip);
		blood.Play ();

		StartCoroutine (removeEnemy());
	}

	IEnumerator removeEnemy()    // to remove enemy after death.
	{
		// we wait seconds after enemy dies.
		yield return new WaitForSeconds (4f);
		// enemy start to sink.
		enemyDisappeared = true;
		// we wait 2 seconds for ...
		yield return new WaitForSeconds (2f);
		// then destroying the enemy.
		Destroy (gameObject);
	}
}
