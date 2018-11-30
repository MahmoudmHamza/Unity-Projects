using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBtn : MonoBehaviour {

    [SerializeField]
    private GameObject weaponObject;

    public GameObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
    }
}
