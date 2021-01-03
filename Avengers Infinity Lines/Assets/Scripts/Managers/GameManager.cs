using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private StonesController stonesController;

    [SerializeField]
    private PlanetController planetController;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private Transform spaceStoneSpawnPoint;

    private AudioManager AudioManager => AudioManager.Instance;

    public EnemyController EnemyController => this.enemyController;

    public UIController UIController => this.uiController;

    private LineCreator LineCreator => LineCreator.Instance;

    public bool IsStoneUsed { get; set; }

    public bool IsSelectingStone { get; set; }

    public bool IsPowerStoneEnabled { get; set; }

    public bool IsSoulStoneEnabled { get; set; }

    private Level levelIndex;

    private Player levelAvenger;

    public void InitializeLevel(LevelEntry levelEntry)
    {
        this.InitializeCommonLevelProperties();

        this.levelIndex = levelEntry.LevelIndex;
        this.levelAvenger = levelEntry.LevelAvenger;

        this.enemyController.EnemyEntires = levelEntry.EnemyUnits.ToList();
        this.stonesController.StoneType = levelEntry.StoneType;
        this.planetController.PlanetName = levelEntry.Planet;

        this.LineCreator.LevelLine = levelEntry.LevelLine;
        this.LineCreator.MaxLength = levelEntry.maxLineLength;
        this.LineCreator.MaxLines = levelEntry.maxLinesCount;

        this.uiController.InitializeGamePlayUI(levelEntry.LevelLine, levelEntry.Planet);
    }

    public override void Awake()
    {
        base.Awake();
        this.InitializeLevel(LevelManager.Instance.SelectedLevel);
    }

    private void InitializeCommonLevelProperties()
    {
        this.IsStoneUsed = false;
        this.IsSelectingStone = false;
        this.IsPowerStoneEnabled = false;
        this.IsSoulStoneEnabled = false;
    }

    private void Start()
    {
        this.SpawnLevelAvenger();
    }

    private void SpawnLevelAvenger()
    {
        var avengerObject = Instantiate(this.levelAvenger.gameObject);
        avengerObject.transform.position = this.playerSpawnPoint.position;

        var avenger = avengerObject.GetComponent<Player>();
        avenger.OnPlayerDestroyed += this.OnPlayerDestroyed;
        avenger.OnStoneCollected += this.OnStoneCollected;
    }

    private void OnPlayerDestroyed()
    {
        this.uiController.ToggleGameStatusPanel(true, this.levelIndex, GameStatus.Lose);
        this.AudioManager.PlaySoundEffect(AudioKey.Lose);
        this.OnGameEnded();
    }

    private void OnStoneCollected()
    {
        this.uiController.ToggleGameStatusPanel(true, this.levelIndex, GameStatus.Win);
        this.AudioManager.PlaySoundEffect(AudioKey.Win);
        LevelManager.Instance.OnLevelCompleted(this.levelIndex);
        this.OnGameEnded();
    }

    public void OnSpaceStoneUsed()
    {
        var avenger = FindObjectOfType<Player>();
        avenger.transform.position = this.spaceStoneSpawnPoint.position;

        this.UIController.ShowSelectedStoneHint(InfinityStoneType.Space, true);
    }

    public void OnSoulStoneUsed()
    {
        this.IsSoulStoneEnabled = true;
        this.EnemyController.StartSoulStoneEffect();
        this.UIController.ShowSelectedStoneHint(InfinityStoneType.Soul);
    }

    public void OnPowerStoneUsed()
    {
        this.IsPowerStoneEnabled = true;
        this.UIController.ShowSelectedStoneHint(InfinityStoneType.Power, true);
    }

    private void OnGameEnded()
    {
        this.LineCreator.DestroyAllLines();
        this.EnemyController.DestroyAllEnemies();
        this.uiController.HideLevelUI();
    }
}