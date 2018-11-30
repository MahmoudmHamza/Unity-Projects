using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenAttack = 1f;

	private Animator anim;
	private bool playerInRange;
	private GameObject player;
	private BoxCollider[] weaponColliders;
	private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
		weaponColliders = GetComponentsInChildren<BoxCollider> ();
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		player = GameManager.instance.Player;
		StartCoroutine (attack ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < range  && enemyHealth.IsAlive) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}
	}

	IEnumerator attack()
	{
		if (playerInRange && !GameManager.instance.GameOver) {
			anim.Play ("Attack");
			yield return new WaitForSeconds (timeBetweenAttack);
		}
		yield return null;
		StartCoroutine (attack ());
	}

	public void EnemyBeginAttack()
	{
		foreach (var weapon in weaponColliders) {
			weapon.enabled = true;
		}
	}
	public void EnemyEndAttack()
	{
		foreach (var weapon in weaponColliders) {
			weapon.enabled = false;
		}
	}
}
