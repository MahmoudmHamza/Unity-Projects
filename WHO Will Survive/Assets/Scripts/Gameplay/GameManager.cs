using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<GameObject> unitPrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject spawnPoint;

    private List<GameObject> unitsList = new List<GameObject>();
    public List<GameObject> UnitsList
    {
        get
        {
            return this.unitsList;
        }
        set
        {
            this.unitsList = value;
        }
    }

    private List<GameObject> timersList = new List<GameObject>();
    public List<GameObject> TimersList
    {
        get
        {
            return this.timersList;
        }
        set
        {
            this.timersList = value;
        }
    }

    public int RecoveredCases { get; set; }

    public int SuspectedCases { get; set; }

    public bool IsSanetizationAvailable { get; private set; }

    public bool IsGameEnded { get; private set; }

    private int currentDifficulty = 0;

    private Timer unitSpawnTimer;

    public override void Awake()
    {
        EventsManager.Instance.OnSanetizationAvailable += this.OnSanetizationAvailable;
        EventsManager.Instance.OnGameEnded += this.OnGameEnded;
        this.InitLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.IsGameEnded = false;
        this.IsSanetizationAvailable = false;
        this.RecoveredCases = 0;
        this.SuspectedCases = 0;

        this.unitSpawnTimer = GetComponent<Timer>();
        this.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsGameEnded)
        {
            return;
        }

        this.HandleRightMouseClick();

        this.HandleSpawnTimer();
    }

    private void InitLevel()
    {
        this.currentDifficulty = PlayerPrefs.GetInt("Difficulty", this.currentDifficulty);
        switch (this.currentDifficulty)
        {
            case 0:
                Debug.Log("Initializing Easy Level");
                LevelManager.Instance.InitEasyLevel();
                break;

            case 1:
                Debug.Log("Initializing Normal Level");
                LevelManager.Instance.InitNormalLevel();
                break;

            case 2:
                Debug.Log("Initializing Hard Level");
                LevelManager.Instance.InitHardLevel();
                break;
        }
    }

    private void HandleSpawnTimer()
    {
        if (this.unitSpawnTimer.Finished)
        {
            this.SpawnUnit();
            this.StartTimer();
        }
    }

    private void StartTimer()
    {
        this.unitSpawnTimer.Duration = LevelManager.Instance.UnitSpawnTimeDuration;
        this.unitSpawnTimer.Run();
    }

    private void HandleRightMouseClick()
    {
        if (Input.GetMouseButtonDown(1) && this.IsSanetizationAvailable)
        {
            AudioManager.Instance.Play(AudioClipName.UltimateSanetization);
            EventsManager.Instance.TriggerOnSanetizationActivated();
            this.unitsList.Clear();
            this.IsSanetizationAvailable = false;
        }
    }

    private void SpawnUnit()
    {
        GameObject newUnit;
        int prefabIndex = Random.Range(0, 100);

        if (prefabIndex < LevelManager.Instance.VirusSpawnPercentage)
        {
            newUnit = this.InitializeUnit(UnitType.Virus);
        }
        else
        {
            newUnit = this.InitializeUnit(UnitType.Timer);
        }
    }

    private GameObject InitializeUnit(UnitType type)
    {
        var newUnit = Instantiate(this.unitPrefabs[0]) as GameObject;
        newUnit.GetComponent<Unit>().Initialize(spawnPoint.transform.position, type);

        if(type != UnitType.Timer)
        {
            this.unitsList.Add(newUnit);
        }
        else
        {
            this.timersList.Add(newUnit);
        }

        return newUnit;
    }

    private void OnGameEnded(bool state)
    {
        if (state)
        {
            AudioManager.Instance.Play(AudioClipName.Win);
        }
        else
        {
            AudioManager.Instance.Play(AudioClipName.Lose);
        }

        DestroyAllUnits();
        this.IsGameEnded = true;
    }

    private void OnSanetizationAvailable()
    {
        this.IsSanetizationAvailable = true;
    }

    private void DestroyAllUnits()
    {
        foreach(var unit in this.unitsList)
        {
            Destroy(unit.gameObject);
        }

        foreach(var timer in this.timersList)
        {
            Destroy(timer.gameObject);
        }

        this.unitsList.Clear();
        this.timersList.Clear();
    }

    private void OnDestroy()
    {
        if(EventsManager.Instance != null)
        {
            EventsManager.Instance.OnGameEnded -= this.OnGameEnded;
        }
    }
}
