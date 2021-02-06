using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages game level behavior
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Timer levelTimer;

    [SerializeField]
    private KnightController knightPrefab;

    [SerializeField]
    private GameObject magicalWeapon;

    [SerializeField]
    private Transform obstaclesContainer;

    [SerializeField]
    private Sprite startPointSprit;

    [SerializeField]
    private GameObject blockerWallTile;

    [SerializeField]
    private GameObject trapTile;

    [SerializeField]
    private MazeGenerator mazeGenerator;

    [SerializeField]
    private int mazeHeight;

    [SerializeField]
    private int mazeWidth;

    public bool IsGameEnded { get; set; }

    public float TimerElapsedSeconds => this.levelTimer.ElapsedSeconds;

    private AudioManager AudioManager => AudioManager.Instance;

    public event Action<GameStatus> OnGameEnded;

    private KnightController knightController;

    void Start()
    {
        this.mazeGenerator.GenerateNewMaze(this.mazeHeight, this.mazeWidth);
        this.SpawnPlayer();
        this.SpawnMagicalWeapon();
        this.SpawnObstacles();
        this.InitializeLevelTimer();
    }

    private void Update()
    {
        if (this.IsGameEnded)
        {
            return;
        }

        if (this.levelTimer.Finished)
        {
            this.TriggerOnGameEnded(GameStatus.Lose);
            Debug.Log("You Lose");
        }
    }

    /// <summary>
    /// Spawns knight prefab at the starting point
    /// </summary>
    private void SpawnPlayer()
    {
        this.knightController = Instantiate(this.knightPrefab, this.mazeGenerator.PathTilesList[0].position, Quaternion.identity);
        this.mazeGenerator.StartPathBlock = this.mazeGenerator.PathTilesList[0].gameObject;
        this.mazeGenerator.StartPathBlock.GetComponentInChildren<SpriteRenderer>().sprite = this.startPointSprit;
    }

    /// <summary>
    /// Spawn the magical orb at some point on the randomly generated path
    /// </summary>
    private void SpawnMagicalWeapon()
    {
        float currentDistance = 0f;
        var randomMaxDistance = UnityEngine.Random.Range(GameConstants.LowerMaxDistanceBound, GameConstants.HigherMaxDistanceBound);

        foreach (var pathTile in this.mazeGenerator.PathTilesList)
        {
            var distance = Vector3.Distance(this.knightController.transform.position, pathTile.position);

            if(distance < currentDistance)
            {
                continue;
            }

            currentDistance = distance;
            if (currentDistance < randomMaxDistance)
            {
                continue;
            }

            var weapon = Instantiate(this.magicalWeapon, pathTile.position, Quaternion.identity);
            this.mazeGenerator.EndPathBlock = pathTile.gameObject;
            break;
        }
    }

    /// <summary>
    /// Spawn maze obstacles randomly
    /// </summary>
    private void SpawnObstacles()
    {
        foreach (var pathTile in this.mazeGenerator.PathTilesList)
        {
            if(pathTile.gameObject == this.mazeGenerator.StartPathBlock || pathTile.gameObject == this.mazeGenerator.EndPathBlock)
            {
                continue;
            }

            var randomValue = UnityEngine.Random.Range(0, 100);
            if (randomValue < GameConstants.ObstacleSpawnPercentage)
            {
                if (UnityEngine.Random.value < 0.5f)
                {
                    Instantiate(this.blockerWallTile, pathTile.position, Quaternion.identity, this.obstaclesContainer);
                }
                else
                {
                    Instantiate(this.trapTile, pathTile.position, Quaternion.identity, this.obstaclesContainer);
                }
            }
        }
    }

    /// <summary>
    /// Initialize level countdown timer properties
    /// </summary>
    private void InitializeLevelTimer()
    {
        this.levelTimer.Duration = GameConstants.CountDownTimerValue;
        this.levelTimer.Run();
    }

    /// <summary>
    /// Triggers <see cref="OnGameEnded"/> action
    /// </summary>
    /// <param name="gameStatus"></param>
    public void TriggerOnGameEnded(GameStatus gameStatus)
    {
        this.OnGameEnded?.Invoke(gameStatus);
        this.IsGameEnded = true;
    }
}
