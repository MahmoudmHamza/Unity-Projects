using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum gameStatus
{
    play, gameover, win ,next
}

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [SerializeField]
    private PlayerBtn playerBtnPressed;

    [SerializeField]
    private Player player;

    [SerializeField]
    private int totalLines = 3;
    [SerializeField]
    private int currentLevel = 1;
    [SerializeField]
    private int maxLevels = 10;

    [SerializeField]
    private GameObject finishObj;
    [SerializeField]
    private GameObject[] obstacleObj;

    [SerializeField]
    private Transform playerSpawnPt;
    [SerializeField]
    private Transform[] finishSpawnPt;


    [SerializeField]
    private Text currentLevelTxt;
    [SerializeField]
    private Image ChampSelectImage;
    [SerializeField]
    private Image GameStatusImage;
    [SerializeField]
    private Text nextLevelBtnText;
    [SerializeField]
    private Text gameStatusImgText;
    [SerializeField]
    private Text avengerSelectText;

    [SerializeField]
    private Image LineSelectImg;
    [SerializeField]
    private Image LevelImg;
    [SerializeField]
    private Image LineReaminImg;

    List<GameObject> FinishPts = new List<GameObject>();
    List<GameObject> ObstacleObjects = new List<GameObject>();

    private int remainingLines;
    private bool levelPassed = false;
    private bool playerOut = false;
    private AudioSource audioSource;

    private gameStatus currentState = gameStatus.play;
    #endregion

    #region Setters & Getters
    public AudioSource AudioSource
    {
        get
        {
            return audioSource;
        }
    }

    public gameStatus CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

    public int TotalLines
    {
        get
        {
            return totalLines;
        }
        set
        {
            totalLines = value;
        }
    }

    public int RemainingLines
    {
        get
        {
            return remainingLines;
        }
        set
        {
            remainingLines = value;
        }
    }

    public bool PlayerOut
    {
        get
        {
            return playerOut;
        }
        set
        {
            playerOut = value;
        }
    }

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
        }
    }
    #endregion


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        remainingLines = totalLines;
        currentLevelTxt.text = "Level : " + currentLevel;
        avengerSelectText.text = "Selected Avenger";
        showMenu();
    }


    void Update()
    {

    }

    #region GameLogic
    public void subtractLines()
    {
        RemainingLines -= 1;
    }

    public void SetCurrentState()
    {
        if (PlayerOut)
        {
            currentState = gameStatus.gameover;
            Debug.Log("you lost");
        }
        else
        { 
            currentState = gameStatus.play;
            Debug.Log("play");
            if (currentLevel < maxLevels)
            {
                currentState = gameStatus.next;
                Debug.Log("next");
            }
            else
            {
                currentState = gameStatus.win;
                Debug.Log("win");
            }
        }
        showMenu();
    }


    public void nextLevelPressed()
    {
        audioSource.PlayOneShot(SoundManager.Instance.ButtonPress);
        ChampSelectImage.gameObject.SetActive(false);
        LineSelectImg.gameObject.SetActive(true);
        LineReaminImg.gameObject.SetActive(true);
        LevelImg.gameObject.SetActive(true);
        switch (currentState)
        {
            case gameStatus.next:
                levelPassed = true;
                break;
        }
        ResetGame();
        totalLines = 3;
        LineCreator.Instance.DestroyAllLines();
        GameStatusImage.gameObject.SetActive(false);
    }

    public void showMenu()
    {

        switch (currentState)
        {
            case gameStatus.play:
                gameStatusImgText.text = "Press Play";
                nextLevelBtnText.text = "Play";
                break;
            case gameStatus.next:
                audioSource.PlayOneShot(SoundManager.Instance.NextLevel);
                currentLevel++;
                gameStatusImgText.text = "You Win";
                nextLevelBtnText.text = "Next Level";
                currentLevelTxt.text = "Level : " + currentLevel;
                break;
            case gameStatus.win:
                audioSource.PlayOneShot(SoundManager.Instance.WinTrack1);
                audioSource.PlayOneShot(SoundManager.Instance.WinTrack2);
                currentLevel = 1;
                gameStatusImgText.text = "You Won";
                nextLevelBtnText.text = "Play Again";
                avengerSelectText.text = "Selected Avenger";
                currentLevelTxt.text = "Level : " + currentLevel;
                ChampSelectImage.gameObject.SetActive(true);
                break;
            case gameStatus.gameover:
                audioSource.PlayOneShot(SoundManager.Instance.LoseTrack1);
                currentLevel = 1;
                gameStatusImgText.text = "You Lost";
                nextLevelBtnText.text = "Try Again";
                avengerSelectText.text = "Selected Avenger";
                currentLevelTxt.text = "Level : " + currentLevel;
                ChampSelectImage.gameObject.SetActive(true);
                break;
        }
        GameStatusImage.gameObject.SetActive(true);
        LineSelectImg.gameObject.SetActive(false);
        LineReaminImg.gameObject.SetActive(false);
        LevelImg.gameObject.SetActive(false);
    }
    #endregion

    #region GameReset & PlayerSpawn
    public void DestroyPlayer(GameObject plyer)
    {
        Destroy(plyer.gameObject);
    }

    public void ResetGame()
    {
        ResetPlayerPosition();
        SetFinishPosition();
        SetObstaclePosition();
    }

    public void ResetPlayerPosition()
    {
        GameObject newPlayer = Instantiate(playerBtnPressed.PlayerObject);
        newPlayer.transform.position = playerSpawnPt.transform.position;
    }

    public void SetFinishPosition()
    {
        GameObject newFinish = Instantiate(finishObj);
        RegisterFinish(newFinish);
        newFinish.transform.position = finishSpawnPt[Random.Range(0, 4)].transform.position;
    }

    public void SetObstaclePosition()
    {
        if (currentLevel > 2 && currentLevel <= 4)
        {
            GameObject newObstacle = Instantiate(obstacleObj[Random.Range(0,4)]);
            RegisterObstacle(newObstacle);
        }
        else if (currentLevel > 4 && currentLevel <= 6)
        {
            for(int i = 0; i < 2; i++)
            {
                GameObject newObstacle = Instantiate(obstacleObj[i]);
                RegisterObstacle(newObstacle);
            }
        }
        else if (currentLevel > 6 && currentLevel <= 8)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject newObstacle = Instantiate(obstacleObj[i]);
                RegisterObstacle(newObstacle);
            }
        }
        else if (currentLevel > 8 && currentLevel <= 9)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject newObstacle = Instantiate(obstacleObj[i]);
                RegisterObstacle(newObstacle);
            }
        }
        else if (currentLevel > 9)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newObstacle = Instantiate(obstacleObj[i]);
                RegisterObstacle(newObstacle);
            }
        }
    }
    #endregion

    #region EnemySpawn
    public void RegisterFinish(GameObject finish)
    {
        FinishPts.Add(finish);
    }

    public void UnRegisterFinish(GameObject finish)
    {
        FinishPts.Remove(finish);
        Destroy(finish.gameObject);
    }

    public void DestroyAllFinishes()
    {
        foreach (GameObject finish in FinishPts)
        {
            Destroy(finish.gameObject);
        }
        FinishPts.Clear();
    }
    #endregion

    #region ObstaclesSpawn
    public void RegisterObstacle(GameObject obs)
    {
        ObstacleObjects.Add(obs);
    }

    public void UnRegisterObstacle(GameObject obs)
    {
        ObstacleObjects.Remove(obs);
        Destroy(obs.gameObject);
    }

    public void DestroyAllObstacles()
    {
        foreach (GameObject obs in ObstacleObjects)
        {
            Destroy(obs.gameObject);
        }
        ObstacleObjects.Clear();
    }
    #endregion

    #region UiControl
    public void selectedChampion(PlayerBtn playerbtn)
    {
        playerBtnPressed = playerbtn;
        SoundManager.Instance.AudSource.PlayOneShot(playerBtnPressed.PlayerTrack);
        avengerSelectText.text = playerBtnPressed.AvengerName;
    }

    public void homePressed()
    {
        audioSource.PlayOneShot(SoundManager.Instance.ButtonPress);
        SceneManager.LoadScene("Game Menu");
    }

    public void exitPressed()
    {
        audioSource.PlayOneShot(SoundManager.Instance.ButtonPress);
        Application.Quit();
    }
    #endregion
}
