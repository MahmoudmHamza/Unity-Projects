using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public float MaxSuspectedCases { get; private set; }

    public float SuspectedCasesIncreaseRate { get; private set; }

    public float SuspectedCasesDecreaseRate { get; private set; }

    public float RecoveredCasesMinValue { get; private set; }

    public float RecoveredCasesMaxValue { get; private set; }

    public float SuspectedCasesMinValue { get; private set; }

    public float SuspectedCasesMaxValue { get; private set; }

    public float TimerMaxValue { get; private set; }

    public float TimerDownValue { get; private set; }

    public float TimerUpValue { get; private set; }

    public float UnitSpeed { get; private set; }

    public float UnitSpawnTimeDuration { get; private set; }

    public int VirusSpawnPercentage { get; private set; }

    #region EASY VALUES
    public void InitEasyLevel()
    {
        this.MaxSuspectedCases = GameConstants.EasyMaxSuspectedCases;
        this.SuspectedCasesIncreaseRate = GameConstants.EasySuspectedCasesIncreaseRate;
        this.SuspectedCasesDecreaseRate = GameConstants.EasySuspectedCasesDecreaseRate;
        this.RecoveredCasesMinValue = GameConstants.EasyRecoveredCasesMinValue;
        this.RecoveredCasesMaxValue = GameConstants.EasyRecoveredCasesMaxValue;
        this.SuspectedCasesMinValue = GameConstants.EasySuspectedCasesMinValue;
        this.SuspectedCasesMaxValue = GameConstants.EasySuspectedCasesMaxValue;
        this.TimerMaxValue = GameConstants.EasyTimerMaxValue;
        this.TimerDownValue = GameConstants.EasyTimerDownValue;
        this.TimerUpValue = GameConstants.EasyTimerUpValue;
        this.UnitSpeed = GameConstants.EasyUnitSpeed;
        this.UnitSpawnTimeDuration = GameConstants.EasyUnitSpawnTimeDuration;
        this.VirusSpawnPercentage = GameConstants.EasyVirusSpawnPercentage;
    }
    #endregion

    #region NORMAL VALUES
    public void InitNormalLevel()
    {
        this.MaxSuspectedCases = GameConstants.NormalMaxSuspectedCases;
        this.SuspectedCasesIncreaseRate = GameConstants.NormalSuspectedCasesIncreaseRate;
        this.SuspectedCasesDecreaseRate = GameConstants.NormalSuspectedCasesDecreaseRate;
        this.RecoveredCasesMinValue = GameConstants.NormalRecoveredCasesMinValue;
        this.RecoveredCasesMaxValue = GameConstants.NormalRecoveredCasesMaxValue;
        this.SuspectedCasesMinValue = GameConstants.NormalSuspectedCasesMinValue;
        this.SuspectedCasesMaxValue = GameConstants.NormalSuspectedCasesMaxValue;
        this.TimerMaxValue = GameConstants.NormalTimerMaxValue;
        this.TimerDownValue = GameConstants.NormalTimerDownValue;
        this.TimerUpValue = GameConstants.NormalTimerUpValue;
        this.UnitSpeed = GameConstants.NormalUnitSpeed;
        this.UnitSpawnTimeDuration = GameConstants.NormalUnitSpawnTimeDuration;
        this.VirusSpawnPercentage = GameConstants.NormalVirusSpawnPercentage;
    }
    #endregion

    #region HARD VALUES
    public void InitHardLevel()
    {
        this.MaxSuspectedCases = GameConstants.HardMaxSuspectedCases;
        this.SuspectedCasesIncreaseRate = GameConstants.HardSuspectedCasesIncreaseRate;
        this.SuspectedCasesDecreaseRate = GameConstants.HardSuspectedCasesDecreaseRate;
        this.RecoveredCasesMinValue = GameConstants.HardRecoveredCasesMinValue;
        this.RecoveredCasesMaxValue = GameConstants.HardRecoveredCasesMaxValue;
        this.SuspectedCasesMinValue = GameConstants.HardSuspectedCasesMinValue;
        this.SuspectedCasesMaxValue = GameConstants.HardSuspectedCasesMaxValue;
        this.TimerMaxValue = GameConstants.HardTimerMaxValue;
        this.TimerDownValue = GameConstants.HardTimerDownValue;
        this.TimerUpValue = GameConstants.HardTimerUpValue;
        this.UnitSpeed = GameConstants.HardUnitSpeed;
        this.UnitSpawnTimeDuration = GameConstants.HardUnitSpawnTimeDuration;
        this.VirusSpawnPercentage = GameConstants.HardVirusSpawnPercentage;
    }
    #endregion
}
