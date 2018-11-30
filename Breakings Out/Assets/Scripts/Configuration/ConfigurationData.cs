using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    static float paddleMoveUnitsPerSecond = 10;
    static float ballImpulseForce = 200;
    static float ballLifeTime = 10;
    static float minSpawnSeconds = 5;
    static float maxSpawnSeconds = 10;
    static int standardBlockPoints = 5;
    static int bonusBlockPoints = 15;
    static int pickupBlockPoints = 10;
    static float freezeDuration = 2;
    static float speedDuration = 2;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    public float BallLifeTime
    {
        get { return ballLifeTime; }
    }

    public float MinSpawnSeconds
    {
        get { return minSpawnSeconds; }
    }
    public float MaxSpawnSeconds
    {
        get { return maxSpawnSeconds; }
    }

    public int StandardBlockPoints
    {
        get
        {
            return standardBlockPoints;
        }
    }

    public int BonusBlockPoints
    {
        get
        {
            return bonusBlockPoints;
        }
    }

    public int PickupBlockPoints
    {
        get
        {
            return pickupBlockPoints;
        }
    }

    public  float FreezeDuration
    {
        get
        {
            return freezeDuration;
        }
    }

    public static float SpeedDuration
    {
        get
        {
            return speedDuration;
        }
    }
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
  
        StreamReader input = null;
        try
        {

            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));


            string names = input.ReadLine();
            string values = input.ReadLine();


            SetConfigDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            // always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }

    void SetConfigDataFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifeTime = float.Parse(values[2]);
        minSpawnSeconds = float.Parse(values[3]);
        maxSpawnSeconds = float.Parse(values[4]);
        standardBlockPoints = int.Parse(values[5]);
        bonusBlockPoints = int.Parse(values[6]);
        pickupBlockPoints = int.Parse(values[7]);
        freezeDuration = float.Parse(values[8]);
        speedDuration = float.Parse(values[9]);
    }
    #endregion
}
