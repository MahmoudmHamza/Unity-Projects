/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Properties
    
    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return 10; }
    }

    public static float BallImpulseForce
    {
        get { return 200; }
    }

    public static float BallLifeTime
    {
        get { return 10; }
    }

    public static float MinSpawnSeconds
    {
        get { return 5; }
    }

    public static float MaxSpawnSeconds
    {
        get { return 10; }
    }

    public static int StandardBlockPoints
    {
        get
        {
            return 5;
        }
    }

    public static int BonusBlockPoints
    {
        get
        {
            return 15;
        }
    }

    public static int PickupBlockPoints
    {
        get
        {
            return 10;
        }
    }

    public static float FreezeDuration
    {
        get
        {
            return 2;
        }
    }

    public static float SpeedDuration
    {
        get
        {
            return 2;
        }
    }
    #endregion

    static ConfigurationData configData;

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configData = new ConfigurationData();
    }
}
