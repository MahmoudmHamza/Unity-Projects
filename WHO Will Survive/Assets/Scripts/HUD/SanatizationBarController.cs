using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanatizationBarController : BarController
{
    private float CurrentValue
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

    private bool isTriggered = false;

    private void Awake()
    {
        EventsManager.Instance.OnUnitDestroyed += this.OnUnitDestroyed;
        EventsManager.Instance.OnUnitEscaped += this.OnUnitEscaped;
        EventsManager.Instance.OnSanetizationActivated += this.OnSanetizationActivated;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ResetBar();
    }

    private void OnUnitDestroyed(int unused)
    {
        this.initialValue++;
        if(this.initialValue >= this.maxValue)
        {
            if (!this.isTriggered)
            {
                EventsManager.Instance.TriggerOnSanetizationAvailable();
                this.isTriggered = true;
            }

            this.initialValue = this.maxValue;
        }

        this.barImage.fillAmount = this.CurrentValue;
    }

    private void OnUnitEscaped(int unused)
    {
        //Dont decrease when the powerup is available
        if (this.isTriggered)
        {
            return;
        }

        this.initialValue--;
        if(this.initialValue < 0)
        {
            this.initialValue = 0;
        }

        this.barImage.fillAmount = this.CurrentValue;
    }

    private void OnSanetizationActivated()
    {
        this.ResetBar();
    }

    private void ResetBar()
    {
        this.isTriggered = false;
        this.initialValue = 0;
        this.maxValue = GameConstants.SanetizationMaxValue;
        this.barImage.fillAmount = this.CurrentValue;
    }

    private void OnDestroy()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnUnitDestroyed -= this.OnUnitDestroyed;
            EventsManager.Instance.OnUnitEscaped -= this.OnUnitEscaped;
            EventsManager.Instance.OnSanetizationActivated -= this.OnSanetizationActivated;
        }
    }
}
