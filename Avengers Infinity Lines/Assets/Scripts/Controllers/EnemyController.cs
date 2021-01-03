using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform enemiesParent;

    public List<EnemyEntry> EnemyEntires{ get; set; }

    public int MaxEnemiesToDestroy { get; private set; }

    public int CurrentDestroyedEnemies { get; private set; }

    private GameManager GameManager => GameManager.Instance;

    private List<Enemy> enemiesList = new List<Enemy>();

    void Start()
    {
        this.SpawnEnemies();
    }
    
    private void SpawnEnemies()
    {
        if(this.EnemyEntires.Count == 0)
        {
            return;
        }

        foreach(var entry in this.EnemyEntires)
        {
            var enemyObject = Instantiate(this.enemyPrefab, this.enemiesParent);
            enemyObject.transform.position = entry.SpawnPostion;

            var enemyUnit = enemyObject.GetComponent<Enemy>();
            enemyUnit.InitializeEnemy(entry.EnemyState, this);

            this.enemiesList.Add(enemyUnit);
        }
    }

    public void OnTimeStoneUsed()
    {
        foreach(var enemy in this.enemiesList)
        {
            enemy.EnemyAnimator.speed = 0.1f;
        }

        this.GameManager.UIController.ShowSelectedStoneHint(InfinityStoneType.Time, true);
    }

    public void StartSoulStoneEffect()
    {
        this.MaxEnemiesToDestroy = (int)(this.enemiesList.Count / 2);
        this.CurrentDestroyedEnemies = 0;
    }

    public void DestroyEnemy(Enemy enemy, bool isSoulEffectActivated = false)
    {
        if (!this.enemiesList.Contains(enemy))
        {
            Debug.LogError("Enemy not found");
            return;
        }

        if (isSoulEffectActivated)
        {
            this.CurrentDestroyedEnemies++;
        }

        this.enemiesList.Remove(enemy);
    }

    public void DestroyAllEnemies()
    {
        foreach(var enemy in this.enemiesList)
        {
            Destroy(enemy.gameObject);
        }

        this.enemiesList.Clear();
    }
}
