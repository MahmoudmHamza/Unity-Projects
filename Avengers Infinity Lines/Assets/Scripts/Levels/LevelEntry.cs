using System;
using UnityEngine;

[Serializable]
public class LevelEntry
{
    public Level LevelIndex;

    public Planet Planet;

    public Player LevelAvenger;

    public InfinityStoneType StoneType;

    public Line LevelLine;

    public int maxLinesCount;

    public int maxLineLength;

    public EnemyEntry[] EnemyUnits;
}
