using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeltingBarController : BarController
{
    /// <summary>
    /// The gradient determining the color of the health.
    /// </summary>
    [SerializeField]
    private Gradient fillGradient;

    private bool isFreezing = false;

    private float CurrentValue
    {
        get
        {
            if(this.maxValue == 0)
            {
                return 0;
            }

            return Mathf.Clamp01(this.initialValue / this.maxValue);
        }
    }

    private void Awake()
    {
        EventsManager.Instance.OnStarHit += OnStarHit;   
    }

    private void Start()
    {
        this.initialValue = GameConstants.StarMeltingValue;
        this.maxValue = GameConstants.StarMeltingValue;
        this.barImage.fillAmount = this.CurrentValue;
        this.barImage.color = this.fillGradient.Evaluate(this.CurrentValue);
    }

    private void LateUpdate()
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

        if (this.initialValue >= this.maxValue)
        {
            return;
        }

        if (this.initialValue >= GameConstants.MeltingThreshold && isFreezing)
        {
            EventsManager.Instance.TriggerOnStarFroze();
            AudioManager.Instance.Play(AudioClipName.StarFreeze);
            isFreezing = false;
        }

        this.initialValue += (Time.deltaTime * GameConstants.StarFreezingRate);
        this.barImage.fillAmount = this.CurrentValue;
        GameManager.Instance.StarController.CurrentMeltingValue = this.initialValue;

        this.barImage.color = this.fillGradient.Evaluate(this.CurrentValue);
    }

    private void OnStarHit(float damage)
    {
        this.initialValue -= damage;

        if(initialValue <= 0)
        {
            initialValue = 0;
            if(isFreezing == false)
            {
                EventsManager.Instance.TriggerOnStarMelted();
                AudioManager.Instance.Play(AudioClipName.StarBurn);
            }
            isFreezing = true;
        }
    }

    private void OnDestroy()
    {
        if(EventsManager.Instance != null)
        {
            EventsManager.Instance.OnStarHit -= OnStarHit;
        }
    }
}
