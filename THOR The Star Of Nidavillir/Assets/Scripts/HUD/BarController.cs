using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    /// <summary>
    /// The slider displaying the unit's health.
    /// </summary>
    [SerializeField]
    protected Image barImage;

    protected float initialValue;

    protected float maxValue;
    
    public virtual void BarBehavior()
    {
    }
}
