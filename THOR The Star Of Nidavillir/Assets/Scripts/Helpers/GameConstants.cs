using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game constants
/// </summary>
public static class GameConstants
{
    // Game Manager constants
    public const float SpawnTimerDuration = 1f;
    public const int MaxAsteroidCount = 10;

    // Asteroids constants
    public const float MinImpulseForce = 1f;
    public const float MaxImpulseForce = 2f;
    public const int MinUpRange = 75;
    public const int MaxUpRange = 105;
    public const int MinDownRange = -75;
    public const int MaxDownRange = -105;
    public const int MinRightRange = 15;
    public const int MaxRightRange = -15;
    public const int MinLeftRange = 165;
    public const int MaxLeftRange = 195;
    public const float AsteroidDamage = 10;
    public const float MiniAsteroidDamage = 1;
    public const int ShrinkedAsteroidCount = 3;

    // Player constants
    public const float ShipThrustForce = 3f;
    public const float PlayerStartingHealth = 100;
    public const float PlayerBulletDamage = 10;

    //Star Constants
    public const float StarMeltingValue = 300;
    public const float StarFreezingRate = 10f;
    public const float MeltingThreshold = 60;
    public const float StarBurnDamage = 0.25f;

    // Bullet constants
    public const float BulletForce = 8f;
    public const float DeathTimerDuration = 0.75f;
    public const int MaxBulletsToOverHeat = 20;
    public const int OverHeatThreshold = 15;
    public const float OverHeatDecreaseRate = 2f;

    //HUD constants
    public const string WinPrefix = "YOU   WIN";
    public const string LosePrefix = "YOU   LOSE";
    public const float StartValueX = 70f;
    public const float StartValueY = 360f;
    public const float EndValueY = 145f;
    public const float BarDecreaseRate = 10f;
}
