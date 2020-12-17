using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarController : BarController
{
    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Timer countDownTimer;

    [SerializeField]
    private List<Image> powerupsPrefabs;

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
        EventsManager.Instance.OnTimerDecreased += OnTimerDecreased;
        EventsManager.Instance.OnTimerIncreased += OnTimerIncreased;
    }

    // Start is called before the first frame update
    void Start()
    {
        countDownTimer.Duration = LevelManager.Instance.TimerMaxValue;
        countDownTimer.Run();

        this.initialValue = LevelManager.Instance.TimerMaxValue;
        this.maxValue = LevelManager.Instance.TimerMaxValue;
        this.barImage.fillAmount = this.CurrentValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTimer.Finished)
        {
            EventsManager.Instance.TriggerOnGameEnded(true);
            return;
        }

        this.BarBehavior();
    }

    private void OnTimerDecreased()
    {
        var powerup = this.powerupsPrefabs[0].gameObject;
        if (!powerup.activeSelf)
        {
            powerup.SetActive(true);
        }

        this.initialValue -= LevelManager.Instance.TimerDownValue;
        if(this.initialValue <= 0)
        {
            EventsManager.Instance.TriggerOnGameEnded(true);
            return;
        }
        else
        {
            this.ResetTimer();
        }

        this.countDownTimer.Duration = this.initialValue;
    }

    private void OnTimerIncreased()
    {
        var powerup = this.powerupsPrefabs[1].gameObject;
        if (!powerup.activeSelf)
        {
            powerup.SetActive(true);
        }

        this.initialValue += LevelManager.Instance.TimerUpValue;
        if(initialValue > maxValue)
        {
            initialValue = maxValue;
        }
        else
        {
            this.ResetTimer();
        }
    }

    private void ResetTimer()
    {
        this.countDownTimer.Stop();
        this.countDownTimer.Duration = this.initialValue;
        this.countDownTimer.Run();
    }

    public override void BarBehavior()
    {
        base.BarBehavior();

        if (this.barImage == null)
        {
            return;
        }

        if (this.initialValue < 0)
        {
            Debug.Log("Timer Has Ended");
            this.initialValue = 0;
            return;
        }

        if(this.initialValue > this.maxValue)
        {
            this.initialValue = this.maxValue;
        }

        this.initialValue -= Time.deltaTime;
        this.timerText.text = ((int)this.initialValue).ToString();
        this.barImage.fillAmount = this.CurrentValue;
    }

    private void OnDestroy()
    {
        if(EventsManager.Instance != null)
        {
            EventsManager.Instance.OnTimerDecreased -= OnTimerDecreased;
            EventsManager.Instance.OnTimerIncreased -= OnTimerIncreased;
        }
    }
}
