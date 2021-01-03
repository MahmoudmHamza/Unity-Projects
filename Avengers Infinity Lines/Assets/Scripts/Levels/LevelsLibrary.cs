using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels Library", menuName = "Libraries/Levels Library")]
public class LevelsLibrary : ScriptableObject
{
    [SerializeField]
    private LevelEntry[] levelEntriesList;

    private IDictionary<Level, LevelEntry> levelByIndex;

    public void InitializeLevelsLibrary()
    {
        this.levelByIndex = new Dictionary<Level, LevelEntry>(this.levelEntriesList.Length);
        foreach (var entry in this.levelEntriesList)
        {
            this.levelByIndex.Add(entry.LevelIndex, entry);
        }
    }

    public LevelEntry GetLevel(Level levelndex)
    {
        if (!this.levelByIndex.TryGetValue(levelndex, out var levelEntry))
        {
            Debug.LogError($"Couldn't find the corresponding level entry for the key {levelndex}");
            return null;
        }

        return levelEntry;
    }
}
