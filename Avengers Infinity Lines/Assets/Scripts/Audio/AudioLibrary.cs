using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Library", menuName = "Libraries/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    [SerializeField]
    private AudioEntry[] audioEntriesList;

    private IDictionary<AudioKey, AudioEntry> audioByKey;

    public void InitializeAudioLibrary()
    {
        this.audioByKey = new Dictionary<AudioKey, AudioEntry>(this.audioEntriesList.Length);
        foreach (var entry in this.audioEntriesList)
        {
            this.audioByKey.Add(entry.AudioKey, entry);
        }
    }

    public AudioEntry GetAudioClip(AudioKey key)
    {
        if (!this.audioByKey.TryGetValue(key, out var audioEntry))
        {
            Debug.LogError($"Couldn't find the corresponding audio entry for the key {key}");
            return null;
        }

        return audioEntry;
    }
}
