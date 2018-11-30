using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour {
    private int remainBalls = 10;
    private int totalScore = 0;

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text remainText;

    [SerializeField]
    private Button HammerBtn;
    [SerializeField]
    private Button AxeBtn;

    void Start () {
        EventsManager.AddPointsListener(increaseScore);
        EventsManager.AddremainListener(subtractBalls);
        scoreText.text = "Score : " + totalScore;
        remainText.text = "Remaining Balls : " + remainBalls;
        HammerBtn.enabled = false;
        AxeBtn.enabled = false;
    }
	
	void Update () {
        scoreText.text = "Score : " + totalScore;
        remainText.text = "Remaining Balls : " + remainBalls;
        CheckScore(totalScore);
    }

    public void subtractBalls(int unused)
    {
        remainBalls -= 1;
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
