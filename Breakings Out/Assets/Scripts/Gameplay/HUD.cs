using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text remainText;

    [SerializeField]
    private Button HammerBtn;

    [SerializeField]
    private Button AxeBtn;

    private int remainBalls = 10;

    private int totalScore = 0;

    void Start ()
    {
        EventsManager.AddPointsListener(increaseScore);
        EventsManager.AddremainListener(subtractBalls);
        scoreText.text = "Score: " + totalScore;
        remainText.text = "Remaining Rocks: " + remainBalls;
        HammerBtn.enabled = false;
        AxeBtn.enabled = false;
    }
	
	private void Update()
    {
        scoreText.text = "Score: " + totalScore;
        remainText.text = "Remaining Rocks: " + remainBalls;
        CheckScore(totalScore);
    }

    public void subtractBalls(int unused)
    {
        this.remainBalls -= 1;
        this.remainBalls = this.remainBalls < 0 ? 0 : this.remainBalls;
    }

    public void increaseScore(int points)
    {
        totalScore += points;
    }

    public void CheckScore(int score)
    {
        if(score >= 100)
        {
            HammerBtn.enabled = true;
            AxeBtn.enabled = true;
        }
        else if(score >= 50)
        {
            HammerBtn.enabled = true;
            AxeBtn.enabled = false;
        }
        else
        {
            HammerBtn.enabled = false;
            AxeBtn.enabled = false;
        }
    }
}
