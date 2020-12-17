using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverHeatBarController : BarController
{
    /// <summary>
    /// The gradient determining the color of the health.
    /// </summary>
    [SerializeField]
    private Gradient fillGradient;

    [SerializeField]
    private GameObject overHeatImage;

    private bool isOverHeated = false;

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
        EventsManager.Instance.OnBulletFired += OnBulletFired;
    }

    private void Start()
    {
        this.initialValue = 0;
        this.maxValue = GameConstants.MaxBulletsToOverHeat;
        this.barImage.fillAmount = this.CurrentValue;
        this.barImage.color = this.fillGradient.Evaluate(this.CurrentValue);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.BarBehavior();
    }

    public override void BarBehavior()
    {
        base.BarBehavior();
        //increase the bar when unit destroyed
        if (this.barImage == null)
        {
            return;
        }

        if (this.initialValue <= 0)
        {
            this.initialValue = 0;
            return;
        }

        if(initialValue < GameConstants.OverHeatThreshold && isOverHeated)
        {
            EventsManager.Instance.TriggerOnOverHeated(false);
            overHeatImage.SetActive(false);
            isOverHeated = false;
        }

        this.initialValue -= Time.deltaTime * GameConstants.OverHeatDecreaseRate;

        this.barImage.fillAmount = this.CurrentValue;
        this.barImage.color = this.fillGradient.Evaluate(this.CurrentValue);
    }

    private void OnBulletFired()
    {
        this.initialValue += 1;

        if(initialValue > this.maxValue)
        {
            this.initialValue = this.maxValue;
            EventsManager.Instance.TriggerOnOverHeated(true);
            AudioManager.Instance.Play(AudioClipName.ShipOverHeat);
            overHeatImage.SetActive(true);
            isOverHeated = true;
        }
    }
}
