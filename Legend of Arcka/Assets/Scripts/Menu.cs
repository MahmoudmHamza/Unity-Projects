using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject ranger;
	[SerializeField] GameObject soldier;

	private Animator heroAnim;
	private Animator rangerAnim;
	private Animator soldierAnim;
	private Animator tankerAnim;

	void Awake()
	{
		Assert.IsNotNull (hero);
		Assert.IsNotNull (tanker);
		Assert.IsNotNull (ranger);
		Assert.IsNotNull (soldier);
	}

	// Use this for initialization
	void Start () {
		heroAnim = hero.GetComponent<Animator> ();
		tankerAnim = tanker.GetComponent<Animator> ();
		rangerAnim = ranger.GetComponent<Animator> ();
		soldierAnim = soldier.GetComponent<Animator> ();

		StartCoroutine (ShowCase ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ShowCase()
	{
		yield return new WaitForSeconds (2f);
		heroAnim.Play ("SpinAttack");

		yield return new WaitForSeconds (1f);
		rangerAnim.Play ("Attack");
		soldierAnim.Play ("Attack");

		yield return new WaitForSeconds (2f);
		tankerAnim.Play ("Attack");

		yield return new WaitForSeconds (3f);
		StartCoroutine (ShowCase ());
	}

	public void Battle()
	{
		SceneManager.LoadScene ("Level 1");   // to switch between scenes.
	}
	public void Quit()
	{
		Application.Quit ();   // won't occur till we export the app.
	}
}
