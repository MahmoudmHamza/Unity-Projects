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

    protected float CurrentValue
    {
        get
        {
            if (this.maxValue == 0)
            {
                return 0;
            }

            return Mathf.Clamp01(this.initialValue / this.maxValue);
        }
    }

    public virtual void BarBehavior()
    {
    }
}
