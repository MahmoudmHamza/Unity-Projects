using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource audioSrc;

    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    private bool isInitialized = false;
    public bool IsInitialized
    {
        get
        {
            return isInitialized;
        }
    }

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (!isInitialized)
        {
            this.InitializeAudioDictionary();
        }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public void InitializeAudioDictionary()
    {
        try
        {
            audioClips.Add(AudioClipName.Win,
                Resources.Load<AudioClip>("Win"));
            audioClips.Add(AudioClipName.Lose,
                Resources.Load<AudioClip>("Lose"));

            audioClips.Add(AudioClipName.ButtonClick,
                Resources.Load<AudioClip>("Button Click"));
            audioClips.Add(AudioClipName.PanelOpened,
                Resources.Load<AudioClip>("Panel Opened"));
            audioClips.Add(AudioClipName.PanelClosed,
                Resources.Load<AudioClip>("Panel Closed"));

            audioClips.Add(AudioClipName.VirusHit,
                Resources.Load<AudioClip>("Virus Hit"));
            audioClips.Add(AudioClipName.VirusEscaped,
                Resources.Load<AudioClip>("Virus Escaped"));
            audioClips.Add(AudioClipName.TimerPicked,
                Resources.Load<AudioClip>("Timer Picked"));
            audioClips.Add(AudioClipName.UltimateSanetization,
                Resources.Load<AudioClip>("Ultimate Sanetization"));

            this.isInitialized = true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public void Play(AudioClipName name)
    {
        this.audioSrc.PlayOneShot(audioClips[name]);
    }
}
