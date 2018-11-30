using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    #region Fields
    [SerializeField]
    private AudioClip nextLevel;
    [SerializeField]
    private AudioClip buttonPress;
    [SerializeField]
    private AudioClip bounceTrack;
    [SerializeField]
    private AudioClip boostTrack;
    [SerializeField]
    private AudioClip normalTrack;
    [SerializeField]
    private AudioClip passThroughTrack;
    [SerializeField]
    private AudioClip winTrack1;
    [SerializeField]
    private AudioClip winTrack2;
    [SerializeField]
    private AudioClip loseTrack1;
    [SerializeField]
    private AudioClip loseTrack2;

    private AudioSource audSource;
    #endregion

    #region Setters & Getters
    public AudioClip NextLevel
    {
        get
        {
            return nextLevel;
        }
    }

    public AudioClip ButtonPress
    {
        get
        {
            return buttonPress;
        }
    }

    public AudioClip BounceTrack
    {
        get
        {
            return bounceTrack;
        }
    }

    public AudioClip BoostTrack
    {
        get
        {
            return boostTrack;
        }
    }

    public AudioClip NormalTrack
    {
        get
        {
            return normalTrack;
        }
    }

    public AudioClip PassThroughTrack
    {
        get
        {
            return passThroughTrack;
        }
    }

    public AudioClip WinTrack1
    {
        get
        {
            return winTrack1;
        }
    }

    public AudioClip WinTrack2
    {
        get
        {
            return winTrack2;
        }
    }

    public AudioClip LoseTrack1
    {
        get
        {
            return loseTrack1;
        }
    }

    public AudioClip LoseTrack2
    {
        get
        {
            return loseTrack2;
        }
    }

    public AudioSource AudSource
    {
        get
        {
            return audSource;
        }
    }
    #endregion

    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }
}
