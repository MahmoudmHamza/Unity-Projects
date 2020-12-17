using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private StarController starController;

    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private List<Direction> directions = new List<Direction>();

    private List<GameObject> asteroidsList = new List<GameObject>();

    private bool isGameFinished = false;

    public List<GameObject> AsteroidsList
    {
        get
        {
            return this.asteroidsList;
        }
    }

    public StarController StarController
    {
        get
        {
            return starController;
        }
    }

    public bool IsGameFinished
    {
        get
        {
            return this.isGameFinished;
        }
    }

    //Spawn timer
    private Timer spawnTimer;

    public override void Awake()
    {
        base.Awake();
        EventsManager.Instance.OnPlayerDestroyed += OnGameEnded;
        EventsManager.Instance.OnWeaponForged += OnGameEnded;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GameConstants.SpawnTimerDuration;
        AudioManager.Instance.Play(AudioClipName.NewGame);
        isGameFinished = false;
        this.SpawnAsteroid();
    }

    private void Update()
    {
        if (isGameFinished)
        {
            return;
        }

        if(asteroidsList.Count < GameConstants.MaxAsteroidCount)
        {
            if (spawnTimer.Finished)
            {
                this.SpawnAsteroid();
            }
        }
    }

    private void OnGameEnded()
    {
        this.DestroyAllAsteroids();
        this.isGameFinished = true;
    }

    /// <summary>
    /// Method spawn an asteroid with iniput direction
    /// </summary>
    /// <param name="direction">input direction</param>
    private void SpawnAsteroid()
    {
        int directionIndex = Random.Range(0, 4);
        Direction asteroidDirection = directions[directionIndex];

        GameObject spawnedAteroid = Instantiate(asteroidPrefab);
        this.RegisterAsteroid(spawnedAteroid);

        float currentAsteroidColliderRadius = spawnedAteroid.GetComponent<ScreenWrapper>().ColliderRadius;
        Vector3 spawnLocation = this.GetSpawnLocationBasedOnDirection(asteroidDirection, currentAsteroidColliderRadius);

        spawnedAteroid.GetComponent<AsteroidController>().Initialize(asteroidDirection, spawnLocation);
        Debug.Log("Spawning Asteroid going to: " + asteroidDirection);

        spawnTimer.Run();
    }

    /// <summary>
    /// Method responsible of getting spawn location for the asteroids
    /// </summary>
    /// <param name="direction">input direction</param>
    /// <param name="colliderRadius">the collider's radius</param>
    /// <returns></returns>
    private Vector3 GetSpawnLocationBasedOnDirection(Direction direction, float colliderRadius)
    {
        if (direction == Direction.Up)
        {
            return new Vector3(0, ScreenUtils.ScreenBottom - colliderRadius, -Camera.main.transform.position.z);
        }
        else if (direction == Direction.Down)
        {
            return new Vector3(0, ScreenUtils.ScreenTop + colliderRadius, -Camera.main.transform.position.z);
        }
        else if (direction == Direction.Right)
        {
            return new Vector3(ScreenUtils.ScreenLeft - colliderRadius, 0, -Camera.main.transform.position.z);
        }
        else if (direction == Direction.Left)
        {
            return new Vector3(ScreenUtils.ScreenRight + colliderRadius, 0, -Camera.main.transform.position.z);
        }
        return Vector3.zero;
    }

    public void RegisterAsteroid(GameObject obj)
    {
        this.asteroidsList.Add(obj);
    }

    public void UnregisterAsteroid(GameObject obj)
    {
        this.asteroidsList.Remove(obj);
        Destroy(obj);
    }

    public void DestroyAllAsteroids()
    {
        foreach(GameObject obj in this.asteroidsList)
        {
            Destroy(obj);
        }

        this.asteroidsList.Clear();
    }
}
