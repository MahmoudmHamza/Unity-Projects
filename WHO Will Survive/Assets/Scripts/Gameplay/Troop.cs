using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : ScriptableObject
{
    [SerializeField]
    private string troopName = "New Name";

    [SerializeField]
    private Sprite troopIcon = null;

    [SerializeField]
    private float damageModifier = 1;

    [SerializeField]
    private float armorModifier = 1;

    [SerializeField]
    private float speedModifier = 1;

    [SerializeField]
    private int jeweleryCarried = 1;
}
