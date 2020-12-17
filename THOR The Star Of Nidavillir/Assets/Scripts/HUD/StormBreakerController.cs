using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBreakerController : BarController
{
    [SerializeField]
    private RectTransform rect;

    private float currentValue;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResetBarValues();
    }

    public void ResetBarValues()
    {
        rect.sizeDelta = new Vector2(GameConstants.StartValueX, GameConstants.StartValueY);
        this.currentValue = GameConstants.StartValueY;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentValue <= GameConstants.EndValueY)
        {
            EventsManager.Instance.TriggerOnWeaponForged();
            AudioManager.Instance.Play(AudioClipName.WeaponForged);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.currentValue -= Time.deltaTime * GameConstants.BarDecreaseRate;
            rect.sizeDelta = new Vector2(GameConstants.StartValueX, this.currentValue);
        }
    }
}
