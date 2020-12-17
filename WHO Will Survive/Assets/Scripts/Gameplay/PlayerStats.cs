using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private string playerName = "New Name";

    [SerializeField]
    private int playerLevel = 1;

    [SerializeField]
    private float playerDamageModifier = 1;

    [SerializeField]
    private float playerEnergyModifier = 1;

    [SerializeField]
    private float playerEnergyRegenraionModifier = 1;
    #endregion

    #region PROPERTIES
    public string PlayerName
    {
        get
        {
            return this.playerName;
        }
    }

    public int PlayerLevel
    {
        get
        {
            return this.playerLevel;
        }
        set
        {
            this.playerLevel = value;
        }
    }

    public float PlayerDamageModifier
    {
        get
        {
            return this.playerDamageModifier;
        }
        set
        {
            this.playerDamageModifier = value;
        }
    }

    public float PlayerEnergyModifier
    {
        get
        {
            return this.playerEnergyModifier;
        }
        set
        {
            this.playerEnergyModifier = value;
        }
    }

    public float PlayerEnergyRegenraionModifier
    {
        get
        {
            return this.playerEnergyRegenraionModifier;
        }
        set
        {
            this.playerEnergyRegenraionModifier = value;
        }
    }
    #endregion
}
