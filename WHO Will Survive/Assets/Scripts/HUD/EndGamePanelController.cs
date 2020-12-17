using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : MonoBehaviour
{
    [SerializeField]
    private Text suspectedText;

    [SerializeField]
    private Text recoveredText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private Text newHighscoreText;

    private int highscore = 0;

    public void InitializePanel()
    {
        this.suspectedText.text = GameConstants.SuspectedCasesPrefix + GameManager.Instance.SuspectedCases.ToString();
        this.recoveredText.text = GameConstants.RecoveredCasesPrefix + GameManager.Instance.RecoveredCases.ToString();

        var score = this.CalculateScore();
        this.scoreText.text = GameConstants.ScorePrefix + score.ToString();

        this.SetHighscore(score);
    }

    private int CalculateScore()
    {
        var score = GameManager.Instance.RecoveredCases;
        if(score < 0)
        {
            return 0;
        }
        return score;
    }

    private void SetHighscore(int currentScore)
    {
        this.highscore = PlayerPrefs.GetInt("Highscore", this.highscore);

        if(highscore == 0 || highscore < currentScore)
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
            this.highscoreText.text = GameConstants.HighscorePrefix + currentScore.ToString();
            this.newHighscoreText.gameObject.SetActive(true);
        }
        else
        {
            this.highscoreText.text = GameConstants.HighscorePrefix + highscore.ToString();
            this.newHighscoreText.gameObject.SetActive(false);
        }
    }
}
