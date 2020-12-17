using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game constants
/// </summary>
public static class GameConstants
{
    public const int SanetizationMaxValue = 20;

    //String constants
    public const string ScoreTextPrefix = "Score: ";
    public const string HealthTextPrefix = " / 100";
    public const string WinPrefix = "YOU WIN";
    public const string LosePrefix = "YOU LOSE";
    public const string DifficultyPrefix = "Difficulty: ";
    public const string RecoveredCasesPrefix = "Recovered Cases: ";
    public const string SuspectedCasesPrefix = "Suspected Cases: ";
    public const string LevelScenePrefix = "Level Scene";
    public const string MainScenePrefix = "Main Scene";
    public const string BordersTag = "Borders";
    public const string ScorePrefix = "Your Score: ";
    public const string HighscorePrefix = "Highscore: ";

    //Easy Value
    public const float EasyMaxSuspectedCases = 50000;
    public const float EasySuspectedCasesIncreaseRate = 35f;
    public const float EasySuspectedCasesDecreaseRate = 60f;
    public const float EasyRecoveredCasesMinValue = 100;
    public const float EasyRecoveredCasesMaxValue = 300;
    public const float EasySuspectedCasesMinValue = 150;
    public const float EasySuspectedCasesMaxValue = 400;
    public const float EasyTimerMaxValue = 80f;
    public const float EasyTimerDownValue = 3f;
    public const float EasyTimerUpValue = 2f;
    public const float EasyUnitSpeed = 1.8f;
    public const float EasyUnitSpawnTimeDuration = 0.5f;
    public const int EasyVirusSpawnPercentage = 75;

    //Normal Value
    public const float NormalMaxSuspectedCases = 40000;
    public const float NormalSuspectedCasesIncreaseRate = 80f;
    public const float NormalSuspectedCasesDecreaseRate = 110f;
    public const float NormalRecoveredCasesMinValue = 50;
    public const float NormalRecoveredCasesMaxValue = 150;
    public const float NormalSuspectedCasesMinValue = 300;
    public const float NormalSuspectedCasesMaxValue = 650;
    public const float NormalTimerMaxValue = 100f;
    public const float NormalTimerDownValue = 2f;
    public const float NormalTimerUpValue = 4f;
    public const float NormalUnitSpeed = 2f;
    public const float NormalUnitSpawnTimeDuration = 0.4f;
    public const int NormalVirusSpawnPercentage = 85;

    //Hard Values
    public const float HardMaxSuspectedCases = 30000;
    public const float HardSuspectedCasesIncreaseRate = 125f;
    public const float HardSuspectedCasesDecreaseRate = 150f;
    public const float HardRecoveredCasesMinValue = 10;
    public const float HardRecoveredCasesMaxValue = 50;
    public const float HardSuspectedCasesMinValue = 450;
    public const float HardSuspectedCasesMaxValue = 900;
    public const float HardTimerMaxValue = 120f;
    public const float HardTimerDownValue = 1f;
    public const float HardTimerUpValue = 3f;
    public const float HardUnitSpeed = 2.1f;
    public const float HardUnitSpawnTimeDuration = 0.3f;
    public const int HardVirusSpawnPercentage = 95;
}
