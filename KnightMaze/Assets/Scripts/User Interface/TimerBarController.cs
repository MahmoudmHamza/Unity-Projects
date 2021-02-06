using UnityEngine.UI;
using UnityEngine;

public class TimerBarController : BarController
{
    [SerializeField]
    private Text timerText;

    private GameManager GameManager => GameManager.Instance;

    void Start()
    {
        this.InitializeTimerBar();
    }

    void Update()
    {
        if (this.GameManager.IsGameEnded)
        {
            return;
        }

        this.HandleTimeTextValue();
        this.HandleTimerBarValue();
    }

    private void InitializeTimerBar()
    {
        this.initialValue = this.GameManager.TimerElapsedSeconds;
        this.maxValue = GameConstants.CountDownTimerValue;
        this.barImage.fillAmount = this.CurrentValue;
    }

    private void HandleTimeTextValue()
    {
        this.timerText.text = this.GameManager.TimerElapsedSeconds < 0 ? "0" : ((int)this.GameManager.TimerElapsedSeconds).ToString();
    }

    private void HandleTimerBarValue()
    {
        if (this.barImage == null)
        {
            return;
        }

        this.initialValue = this.GameManager.TimerElapsedSeconds;
        this.barImage.fillAmount = this.CurrentValue;
    }
}
