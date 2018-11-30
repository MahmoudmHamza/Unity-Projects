using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    private AudioSource audSource;

    [SerializeField]
    private AudioClip btnClick;
    [SerializeField]
    private AudioClip btnClick2;


    [SerializeField]
    private AudioClip standardBlockHit;
    [SerializeField]
    private AudioClip bonusBlockHit;
    [SerializeField]
    private AudioClip freezeBlockHit;
    [SerializeField]
    private AudioClip speedBlockHit;

    [SerializeField]
    private AudioClip weaponChange;
    [SerializeField]
    private AudioClip weaponChange2;
    [SerializeField]
    private AudioClip kingKilled;

    [SerializeField]
    private List<AudioClip> enemySound;
    [SerializeField]
    private List<AudioClip> playerSound;

    public AudioClip BtnClick
    {
        get
        {
            return btnClick;
        }
    }

    public AudioClip BtnClick2
    {
        get
        {
            return btnClick2;
        }
    }

    public AudioClip StandardBlockHit
    {
        get
        {
            return standardBlockHit;
        }
    }

    

    public AudioClip BonusBlockHit
    {
        get
        {
            return bonusBlockHit;
        }
    }

    public AudioClip FreezeBlockHit
    {
        get
        {
            return freezeBlockHit;
        }
    }

    public AudioClip SpeedBlockHit
    {
        get
        {
            return speedBlockHit;
        }
    }

    public AudioClip WeaponChange
    {
        get
        {
            return weaponChange;
        }
    }

    public AudioClip WeaponChange2
    {
        get
        {
            return weaponChange2;
        }
    }

    public AudioClip KingKilled
    {
        get
        {
            return kingKilled;
        }
    }

    public List<AudioClip> EnemySound
    {
        get
        {
            return enemySound;
        }
    }

    public List<AudioClip> PlayerSound
    {
        get
        {
            return playerSound;
        }
    }

    public AudioSource AudSource
    {
        get
        {
            return audSource;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) // if anybody want to creat another game it destroys itself.
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // doesnt destroy game while switching between scenes , maybe useful later.
    }

    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }
}
