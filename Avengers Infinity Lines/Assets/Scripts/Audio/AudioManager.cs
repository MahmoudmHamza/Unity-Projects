using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioLibrary audioLibrary;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        this.audioLibrary.InitializeAudioLibrary();
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public void PlaySoundEffect(AudioKey key)
    {
        var clipEntry = this.audioLibrary.GetAudioClip(key);
        this.audioSource.PlayOneShot(clipEntry.AudioClip);
    }

    /// <summary>
    /// Plays the given audio clip
    /// </summary>
    public void PlayAudioClip(AudioClip clip)
    {
        this.audioSource.PlayOneShot(clip);
    }
}
