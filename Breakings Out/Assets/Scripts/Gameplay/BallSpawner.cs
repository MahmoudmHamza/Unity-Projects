using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameStatus { Play , Win , Lose}

public class BallSpawner : MonoBehaviour {

    BallsRemainingEvent ballRemainingEvent = new BallsRemainingEvent();

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private Ball ball;

    [SerializeField]
    GameObject spwanPt;

    [SerializeField]
    Text gameStatusText;
    [SerializeField]
    Image gameStatusImg;


    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;
    Timer respawnTimer;
    Timer speedTimer;

    float minSec = ConfigurationUtils.MinSpawnSeconds;
    float maxSec = ConfigurationUtils.MaxSpawnSeconds;
    public List<GameObject> ballList = new List<GameObject>();

    private int ballDestroyed = 0;
    private int blocksDestroyed = 0;

    [SerializeField]
    private int totalBalls = 10;
    private GameStatus gameStatus = GameStatus.Play;

    private bool ballSpawned = false;
    private bool ballDead = false;
    private bool ballOut = false;
    private bool gameEnded = false;
    private bool buttonPressed = true;
    private bool hitKing = false;
    private bool speedy = false;

    public bool BallDead
    {
        get
        {
            return ballDead;
        }
        set
        {
            ballDead = value;
        }
    }

    public bool BallOut
    {
        get
        {
            return ballOut;
        }
        set
        {
            ballOut = value;
        }
    }

    public bool BallSpawned
    {
        get
        {
            return ballSpawned;
        }
        set
        {
            ballSpawned = value;
        }
    }


    public bool HitKing
    {
        get
        {
            return hitKing;
        }
        set
        {
            hitKing = value;
        }
    }

    void Start () {
        Time.timeScale = 1f;

        gameStatusImg.gameObject.SetActive(false);

        EventsManager.AddRemainInvoker(this);
        EventsManager.AddSpeedListener(SpeedEffect);
        EventsManager.AddBlocksListener(DestroyedBlocksIncrease);

        speedTimer = gameObject.AddComponent<Timer>();
        respawnTimer = gameObject.AddComponent<Timer>();
        respawnTimer.Duration = Random.Range(minSec,maxSec);
        respawnTimer.Run();

        FirstSpawn();
    }

    void FixedUpdate()
    {
        if (speedy)
        {
            if (speedTimer.Finished)
            {
                Time.timeScale = 1f;
                Debug.Log("RUN");
            }
        }
    }

    void Update() {
        if(!hitKing)
        {
            gameStatus = GameStatus.Play;
            GameState();
        }
        else
        {
            gameStatus = GameStatus.Win;
            showMenu();
        }
    }

    public void BallSpawn()
    {
        ballSpawned = true;
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = spwanPt.transform.position;
        RegisterBall(newBall);
        ballRemainingEvent.Invoke(0);
    }

    public void FirstSpawn()
    {
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = spwanPt.transform.position;
        RegisterBall(newBall);
        ballRemainingEvent.Invoke(0);
    }

    public void RegisterBall(GameObject ball)
    {
        ballList.Add(ball);
    }

    public void UnRegisterBall(GameObject ball)
    {
        ballList.Remove(ball);
        Destroy(ball.gameObject);
    }

    public void DestroyAllBalls()
    {
        foreach (GameObject ball in ballList)
        {
            Destroy(ball.gameObject);
        }
        ballList.Clear();
    }

    public void BallsDestroyedIncrease()
    {
        ballDestroyed += 1;
    }

    public void GameState()
    {
        if (ballDestroyed < totalBalls)
        {
            if (!ballDead || !ballOut)
            {
                if (respawnTimer.Finished && !ballSpawned)
                {
                    BallSpawn();
                    respawnTimer.Duration = Random.Range(minSec, maxSec);
                    respawnTimer.Run();
                    ballSpawned = false;
                }
            }
            else
            {
                ballOut = false;
                ballDead = false;
            }
        }
        else
        {
            gameStatus = GameStatus.Lose;
            showMenu();
        }
    }

    public void DestroyedBlocksIncrease(int unused)
    {
        blocksDestroyed += 1;
    }

    public void BallRemainingEventListener(UnityAction<int> listener)
    {
        ballRemainingEvent.AddListener(listener);
    }

    public void showMenu()
    {
        Time.timeScale = 0;
        switch (gameStatus)
        {
            case GameStatus.Win:
                gameStatusImg.gameObject.SetActive(true);
                gameStatusText.text = "You Win";
                DestroyAllBalls();
                break;
            case GameStatus.Lose:
                gameStatusImg.gameObject.SetActive(true);
                gameStatusText.text = "Game Over";
                DestroyAllBalls();
                break;
            default:
                gameStatusImg.gameObject.SetActive(false);
                break;
        }
    }

    void SpeedEffect(float speedTime)
    {
        speedTimer.Duration = speedTime;
        speedTimer.Run();
        Time.timeScale = 2f;
        speedy = true;
    }
}
