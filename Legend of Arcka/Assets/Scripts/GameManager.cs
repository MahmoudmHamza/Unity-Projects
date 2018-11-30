using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] GameObject player;
	[SerializeField] GameObject[] spawnPoints;
	[SerializeField] GameObject[] powerSpawnPoints;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject ranger;
	[SerializeField] GameObject arrow;
	[SerializeField] GameObject healthPowerUp;
	[SerializeField] GameObject speedPowerUp;
	[SerializeField] Text levelText;
	[SerializeField] Text endGameText;
	[SerializeField] int maxPowerUps = 5;
	[SerializeField] int finalLevel = 15;

	private bool gameOver = false;
	private int currentLevel;
	private float generatedSpawnTime = 1f; // to indicate time between  spawns.
	private float currentSpawnTime = 0;
	private float powerUpSpawnTime = 25f;
	private float currentPowerSpawnTime = 0;
	private int currentPowerUps = 0;

	private GameObject newEnemy;
	private GameObject newPowerUp;

	private List <EnemyHealth> enemies = new List<EnemyHealth>();
	private List <EnemyHealth> killedEnemies = new List<EnemyHealth>();

	public void RegisterEnemy ( EnemyHealth enemy )
	{
		enemies.Add (enemy);
	}
	public void KilledEnemy ( EnemyHealth enemy )
	{
		killedEnemies.Add (enemy);
	}
	public void RegisterPowerUp ()
	{
		currentPowerUps++;
	}
	public void unRegisterPowerUp ()
	{
		currentPowerUps -= 1;
	}

	public bool GameOver {
		get { return gameOver; }
	}
	public GameObject Arrow {
		get { return arrow; }
	}

	public GameObject Player {
		get { return player; }
	}

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		currentLevel = 1;
		StartCoroutine (spawn ());
		StartCoroutine (PowerUpSpawn ());
		endGameText.GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpawnTime += Time.deltaTime;
		currentPowerSpawnTime += Time.deltaTime;
	}

	public void PlayerHit ( int CurrentHP )
	{
		if (CurrentHP > 0) {
			gameOver = false;
		} else {
			gameOver = true;
			StartCoroutine (endGame ("Defeat"));
		}
	}

	IEnumerator spawn()
	{
		if (currentSpawnTime > generatedSpawnTime) {
			currentSpawnTime = 0;

			if (enemies.Count < currentLevel) {
				int randomNumber = Random.Range (0, spawnPoints.Length - 1);
				GameObject spawnLocation = spawnPoints [randomNumber];

				int randomEnemy = Random.Range (0, 6);
				if (randomEnemy == 0) {
					newEnemy = Instantiate (soldier) as GameObject;
				}
				if (randomEnemy == 1) {
					newEnemy = Instantiate (soldier) as GameObject;
				}
				if (randomEnemy == 2) {
					newEnemy = Instantiate (soldier) as GameObject;
				}
				if (randomEnemy == 3) {
					newEnemy = Instantiate (ranger) as GameObject;
				}
				if (randomEnemy == 4) {
					newEnemy = Instantiate (ranger) as GameObject;
				}
				if (randomEnemy == 5) {
					newEnemy = Instantiate (tanker) as GameObject;
				}

				newEnemy.transform.position = spawnLocation.transform.position;
			}
		}
		if (killedEnemies.Count == currentLevel && currentLevel != finalLevel) {
			currentLevel++;
			levelText.text = "Level " + currentLevel;
			enemies.Clear();
			killedEnemies.Clear ();
			yield return new WaitForSeconds (3f); //break between levels.
		}
		if (killedEnemies.Count == finalLevel) {
			StartCoroutine (endGame ("Victory!"));
		}
		yield return null;
		StartCoroutine (spawn ());
	}

	IEnumerator PowerUpSpawn()
	{
		if (currentPowerSpawnTime > powerUpSpawnTime) {
			currentPowerSpawnTime = 0;
			if (currentPowerUps < maxPowerUps) {
				int randomNumber = Random.Range (0, powerSpawnPoints.Length - 1);
				GameObject spawnLocation = powerSpawnPoints [randomNumber];
				int randomPowerUp = Random.Range (0, 4);
				if (randomPowerUp == 0) {
				    newPowerUp = Instantiate (healthPowerUp) as GameObject;
				} else if (randomPowerUp == 1) {
					newPowerUp = Instantiate (speedPowerUp) as GameObject;
				}
				else if (randomPowerUp == 2) {
					newPowerUp = Instantiate (healthPowerUp) as GameObject;
				}
				else if (randomPowerUp == 3) {
					newPowerUp = Instantiate (healthPowerUp) as GameObject;
				}
				newPowerUp.transform.position = spawnLocation.transform.position;
			}
		}
		yield return null;
		StartCoroutine (PowerUpSpawn ());
	}

	IEnumerator endGame(string outCome)
	{
		endGameText.text = outCome;
		endGameText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds (3f);

		SceneManager.LoadScene ("GameMenu");
	}
}