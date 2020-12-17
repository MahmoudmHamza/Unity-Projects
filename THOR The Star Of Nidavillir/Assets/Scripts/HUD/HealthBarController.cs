using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : BarController
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

    private void Awake()
    {
        EventsManager.Instance.OnDamageTaken += OnDamageTaken;
    }

    private void Start()
    {
        this.initialValue = PlayerManager.Instance.CurrentHealth;
        this.maxValue = PlayerManager.Instance.CurrentHealth;
        this.barImage.fillAmount = this.CurrentValue;
    }

    public override void BarBehavior()
    {
        base.BarBehavior();
        //increase the bar when unit destroyed
        if (this.barImage == null)
        {
            return;
        }

        this.barImage.fillAmount = this.CurrentValue;
    }

    private void OnDamageTaken(float damage)
    {
        this.initialValue -= damage;
        BarBehavior();
    }

    private void OnDestroy()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnDamageTaken -= OnDamageTaken;
        }
    }
}
