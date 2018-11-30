using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour {

	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenAttack = 1f;
	[SerializeField] Transform fireLocation;

	private Animator anim;
	private bool playerInRange;
	private GameObject player;
	private EnemyHealth enemyHealth;
	private GameObject arrow;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		player = GameManager.instance.Player;
		arrow = GameManager.instance.Arrow;
		StartCoroutine (attack ());
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < range  && enemyHealth.IsAlive) {
			playerInRange = true;
			anim.SetBool ("PlayerInRange", true);
			RotateTowards (player.transform);
		} else {
			playerInRange = false;
			anim.SetBool ("PlayerInRange", false);
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

	private void RotateTowards(Transform player){
		Vector3 direction = (player.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	public void FireArrow()
	{
		GameObject newArrow = Instantiate (arrow) as GameObject;
		newArrow.transform.position = fireLocation.position;  //arrow fire location.
		newArrow.transform.rotation = transform.rotation;    // set arrow rotation.
		newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
	}
}
