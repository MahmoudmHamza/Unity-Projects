using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasesBarController : BarController
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
        EventsManager.Instance.OnUnitDestroyed += this.OnUnitDestroyed;
        EventsManager.Instance.OnUnitEscaped += this.OnUnitEscaped;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.initialValue = GameManager.Instance.SuspectedCases;
        this.maxValue = LevelManager.Instance.MaxSuspectedCases;
        this.barImage.fillAmount = this.CurrentValue;
    }

    // Update is called once per frame
    void Update()
    {
        this.BarBehavior();
    }

    public override void BarBehavior()
    {
        base.BarBehavior();

        if (this.barImage == null)
        {
            return;
        }

        if (this.initialValue >= this.maxValue)
        {
            EventsManager.Instance.TriggerOnGameEnded(false);
            return;
        }

        if(GameManager.Instance.UnitsList.Count > 0)
        {
            this.initialValue += Time.deltaTime
                * LevelManager.Instance.SuspectedCasesIncreaseRate
                * (float)GameManager.Instance.UnitsList.Count;

            this.barImage.fillAmount = this.CurrentValue;
            GameManager.Instance.SuspectedCases = (int)this.initialValue;
        }
        else
        {
            if(this.initialValue <= 0)
            {
                this.initialValue = 0;
            }
            else
            {
                this.initialValue -= Time.deltaTime 
                    * LevelManager.Instance.SuspectedCasesDecreaseRate;
            }

            this.barImage.fillAmount = this.CurrentValue;
            GameManager.Instance.SuspectedCases = (int)this.initialValue;
        }
    }

    /// <summary>
    /// Handles unit escaped trigger
    /// </summary>
    /// <param name="monuments"></param>
    private void OnUnitEscaped(int cases)
    {
        this.initialValue += cases;
        if (this.initialValue >= this.maxValue)
        {
            EventsManager.Instance.TriggerOnGameEnded(false);
        }
    }

    /// <summary>
    /// Handles unit destruction trigger
    /// </summary>
    /// <param name=""></param>
    private void OnUnitDestroyed(int cases)
    {
        this.initialValue -= cases;
        if (this.initialValue <= 0)
        {
            this.initialValue = 0;
        }
    }

}
