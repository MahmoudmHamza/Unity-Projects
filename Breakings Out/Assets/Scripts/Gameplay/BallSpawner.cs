using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public enum GameStatus
{
    Play,
    Win,
    Lose
}

public class BallSpawner : MonoBehaviour
{
    private float minSec = ConfigurationUtils.MinSpawnSeconds;

    private float maxSec = ConfigurationUtils.MaxSpawnSeconds;

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

    [SerializeField]
    private int totalBalls = 10;

    private GameStatus gameStatus = GameStatus.Play;

    public List<GameObject> ballList = new List<GameObject>();

    BallsRemainingEvent ballRemainingEvent = new BallsRemainingEvent();

    private int ballDestroyed = 0;

    private int blocksDestroyed = 0;

    private bool isGameEnded = false;

    private bool isRockActive = false;

    private bool isSpeedEffectEnabled = false;

    Timer speedTimer;

    private Coroutine activeCoroutine;

    private void Start()
    {
        this.InitializeSceneProperties();
        this.InitializeEvents();

        this.SpawnRock();
    }

    private void FixedUpdate()
    {
        if (this.isGameEnded)
        {
            return;
        }

        this.HandleSpeedEffect();
    }

    private void Update()
    {
        if (this.isGameEnded)
        {
            return;
        }

        this.CheckGameState();
    }

    private void HandleSpeedEffect()
    {
        if (!this.isSpeedEffectEnabled)
        {
            return;
        }

        if (this.speedTimer.Finished)
        {
            Time.timeScale = 1f;
        }
    }

    private void InitializeSceneProperties()
    {
        Time.timeScale = 1f;
        this.gameStatus = GameStatus.Play;
        this.isGameEnded = false;
        this.gameStatusImg.gameObject.SetActive(false);
        this.speedTimer = this.gameObject.AddComponent<Timer>();
    }

    private void InitializeEvents()
    {
        EventsManager.AddRemainInvoker(this);
        EventsManager.AddSpeedListener(SpeedEffect);
        EventsManager.AddBlocksListener(DestroyedBlocksIncrease);
    }

    public void SpawnRock()
    {
        var newBall = Instantiate(this.ballPrefab);
        newBall.transform.position = spwanPt.transform.position;

        this.RegisterBall(newBall);
        this.ballRemainingEvent.Invoke(0);

        this.isRockActive = true;
    }

    public void OnBallDestroyed()
    {
        if (this.activeCoroutine != null)
        {
            return;
        }

        this.isRockActive = false;
        this.ballDestroyed++;
        this.activeCoroutine = this.StartCoroutine(this.WaitThenSpawnBall());
    }

    public void OnKingKilled()
    {
        this.isGameEnded = true;
        this.gameStatus = GameStatus.Win;
        this.ConcludeGame();
    }

    private IEnumerator WaitThenSpawnBall()
    {
        yield return new WaitForSeconds(1);

        if (!this.isGameEnded && !this.isRockActive)
        {
            this.SpawnRock();
        }

        this.activeCoroutine = null;
    }

    private void CheckGameState()
    {
        if (this.ballDestroyed < this.totalBalls)
        {
            return;
        }

        this.isGameEnded = true;
        this.gameStatus = GameStatus.Lose;
        this.ConcludeGame();
    }

    private void SpeedEffect(float speedDuration)
    {
        this.speedTimer.Duration = speedDuration;
        this.speedTimer.Run();
        Time.timeScale = 1.5f;
        this.isSpeedEffectEnabled = true;
    }

    public void DestroyedBlocksIncrease(int unused)
    {
        this.blocksDestroyed += 1;
    }

    public void BallRemainingEventListener(UnityAction<int> listener)
    {
        this.ballRemainingEvent.AddListener(listener);
    }

    public void ConcludeGame()
    {
        Time.timeScale = 0;

        switch (this.gameStatus)
        {
            case GameStatus.Win:
                this.InitializeGameStatusPanel("You Win");
                break;

            case GameStatus.Lose:
                this.InitializeGameStatusPanel("Game Over");
                break;

            default:
                this.gameStatusImg.gameObject.SetActive(false);
                break;
        }
    }

    private void InitializeGameStatusPanel(string content)
    {
        this.gameStatusText.text = content;
        this.gameStatusImg.gameObject.SetActive(true);
        this.DestroyAllBalls();
    }

    public void RegisterBall(GameObject ball)
    {
        this.ballList.Add(ball);
    }

    public void UnRegisterBall(GameObject ball)
    {
        this.ballList.Remove(ball);
        Destroy(ball.gameObject);
    }

    public void DestroyAllBalls()
    {
        foreach (GameObject ball in ballList)
        {
            Destroy(ball.gameObject);
        }

        this.ballList.Clear();
    }
}
