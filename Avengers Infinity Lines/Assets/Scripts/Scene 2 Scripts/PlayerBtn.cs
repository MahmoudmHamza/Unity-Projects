using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBtn : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private AudioClip playerTrack;
    [SerializeField]
    private string avengerName;
    #endregion

    #region Setters & Getters
    public GameObject PlayerObject
    {
        get
        {
            return playerObject;
        }
    }

    public AudioClip PlayerTrack
    {
        get
        {
            return playerTrack;
        }
    }

    public string AvengerName
    {
        get
        {
            return avengerName;
        }
    }
    #endregion

}
