using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private LevelsLibrary levelsLibrary;

    public LevelEntry SelectedLevel { get; private set; }

    public event Action OnResetProgress;

    private Dictionary<Level, string> prefKeyByLevel = new Dictionary<Level, string>
    {
        {Level.Level0, GameConstants.MindStonePrefKey },
        {Level.Level1, GameConstants.TimeStonePrefKey },
        {Level.Level2, GameConstants.RealityStonePrefKey },
        {Level.Level3, GameConstants.PowerStonePrefKey },
        {Level.Level4, GameConstants.EndGamePrefKey },
        {Level.Level5, GameConstants.SoulStonePrefKey },
        {Level.Level6, GameConstants.SpaceStonePrefKey },
    };

    private Dictionary<Level, InfinityStoneType> obtainedStoneByLevel = new Dictionary<Level, InfinityStoneType>
    {
        {Level.Level0, InfinityStoneType.Mind },
        {Level.Level1, InfinityStoneType.Time },
        {Level.Level2, InfinityStoneType.Reality },
        {Level.Level3, InfinityStoneType.Power },
        {Level.Level4, InfinityStoneType.Guantlet },
        {Level.Level5, InfinityStoneType.Soul},
        {Level.Level6, InfinityStoneType.Space },
    };

    private Dictionary<InfinityStoneType, string> stoneDescriptionByStone = new Dictionary<InfinityStoneType, string>
    {
        {InfinityStoneType.Guantlet, GameConstants.EndGameDescriptionKey },
        {InfinityStoneType.Mind, GameConstants.MindStoneDescriptionKey },
        {InfinityStoneType.Time, GameConstants.TimeStoneDescriptionKey },
        {InfinityStoneType.Reality, GameConstants.RealityStoneDescriptionKey },
        {InfinityStoneType.Power, GameConstants.PowerStoneDescriptionKey },
        {InfinityStoneType.Soul, GameConstants.SoulStoneDescriptionKey },
        {InfinityStoneType.Space, GameConstants.SpaceStoneeDescriptionKey },
    };

    private List<Level> completedLevels = new List<Level>();

    public override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }

    void Start()
    {
        this.InitializePlayerProgress();
        this.levelsLibrary.InitializeLevelsLibrary();
    }

    private void InitializePlayerProgress()
    {
        foreach(var entry in this.prefKeyByLevel)
        {
            if (!PlayerPrefs.HasKey(entry.Value))
            {
                PlayerPrefs.SetString(entry.Value, "NotCompleted");
            }
            else
            {
                if(PlayerPrefs.GetString(entry.Value) == "Completed")
                {
                    this.completedLevels.Add(entry.Key);
                }
            }
        }
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        switch (loadedScene.name)
        {
            case "Game Menu":
                this.SelectedLevel = null;
                break;
            case "Game Play":
                break;
        }
    }

    public void LoadLevel(Level index)
    {
        this.SelectedLevel = this.levelsLibrary.GetLevel(index);
        SceneManager.LoadScene("Game Play");
    }

    public void OnLevelCompleted(Level level)
    {
        var prefKey = this.prefKeyByLevel[level];
        PlayerPrefs.SetString(prefKey, "Completed");
        this.completedLevels.Add(level);
    }

    public void OnResetProgressClicked()
    {
        PlayerPrefs.DeleteAll();
        this.completedLevels.Clear();
        this.InitializePlayerProgress();
        this.OnResetProgress?.Invoke();
    }

    public List<InfinityStoneType> GetObtainedStoneList()
    {
        var stonesList = new List<InfinityStoneType>();
        foreach(var level in this.completedLevels)
        {
            var stone = this.obtainedStoneByLevel[level];
            stonesList.Add(stone);
        }

        return stonesList;
    }

    public string GetStoneDescription(InfinityStoneType stone)
    {
        return this.stoneDescriptionByStone[stone];
    }

    public string GetDescriptionByLevel(Level level)
    {
        var levelStone = this.obtainedStoneByLevel[level];
        return this.GetStoneDescription(levelStone);
    }
}
