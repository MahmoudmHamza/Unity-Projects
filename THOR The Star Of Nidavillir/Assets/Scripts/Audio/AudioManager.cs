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

    //static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    private bool isInitialized = false;
    public bool IsInitialized
    {
        get
        {
            return this.isInitialized;
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
            audioClips.Add(AudioClipName.NewGame,
            Resources.Load<AudioClip>("New Game"));
            audioClips.Add(AudioClipName.Win,
                Resources.Load<AudioClip>("Win"));
            audioClips.Add(AudioClipName.Lose,
                Resources.Load<AudioClip>("Lose"));
            audioClips.Add(AudioClipName.ButtonClick,
                Resources.Load<AudioClip>("Button Click"));
            audioClips.Add(AudioClipName.WeaponForged,
                Resources.Load<AudioClip>("Weapon Forged"));

            audioClips.Add(AudioClipName.AsteroidExplosion,
                Resources.Load<AudioClip>("Asteroid Explosion"));
            audioClips.Add(AudioClipName.AsteroidHit,
                Resources.Load<AudioClip>("Asteroid Hit"));

            audioClips.Add(AudioClipName.ShipHit,
                Resources.Load<AudioClip>("Ship Hit"));
            audioClips.Add(AudioClipName.ShipFire,
                Resources.Load<AudioClip>("Ship Fire"));
            audioClips.Add(AudioClipName.ShipDestruction,
                Resources.Load<AudioClip>("Ship Destruction"));
            audioClips.Add(AudioClipName.ShipOverHeat,
                Resources.Load<AudioClip>("Ship OverHeat"));

            audioClips.Add(AudioClipName.StarBurn,
                Resources.Load<AudioClip>("Star Burn"));
            audioClips.Add(AudioClipName.StarFreeze,
                Resources.Load<AudioClip>("Star Freeze"));
            audioClips.Add(AudioClipName.StarHit,
                Resources.Load<AudioClip>("Star Hit"));

            this.isInitialized = true;
        }
        catch(System.Exception e)
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
