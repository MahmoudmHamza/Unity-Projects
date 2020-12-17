using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private float currentHealth;

    public float CurrentHealth
    {
        get
        {
            return this.currentHealth;
        }
        set
        {
            this.currentHealth = value;
        }
    }

    public override void Awake()
    {
        base.Awake();
        EventsManager.Instance.OnDamageTaken += OnDamageTaken;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GameConstants.PlayerStartingHealth;
    }

    private void OnDamageTaken(float damage)
    {
        if (GameManager.Instance.IsGameFinished)
        {
            return;
        }

        this.currentHealth -= damage;

        if(this.currentHealth <= 0)
        {
            EventsManager.Instance.TriggerOnPlayerDestroyed();
        }
    }
}
