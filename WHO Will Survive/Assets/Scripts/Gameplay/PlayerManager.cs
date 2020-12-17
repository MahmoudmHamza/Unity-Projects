using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private PlayerStats playerStats;
    private float currentEnergy;
    private float maxEnergy;

    public PlayerStats PlayerStats
    {
        get
        {
            return this.playerStats;
        }
    }

    public float CurrentEnergy
    {
        get
        {
            return this.currentEnergy;
        }
        set
        {
            this.currentEnergy = value;
        }
    }

    public float MaxEnergy
    {
        get
        {
            return this.maxEnergy;
        }
    }

    private void Awake()
    {
        this.playerStats = GetComponent<PlayerStats>();
        Initialize();
    }

    private void Initialize()
    {
        currentEnergy = playerStats.PlayerEnergyModifier;
        maxEnergy = playerStats.PlayerEnergyModifier;
    }

    public bool IsOutOfEnergy(float cost)
    {
        if(this.currentEnergy < cost)
        {
            // show out of energy indicator
            Debug.Log("Skill execute failed due to lack of energy");
            return true;
        }
        return false;
    }
}
