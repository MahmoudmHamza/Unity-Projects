using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainmenuController : MonoBehaviour
{
    [SerializeField]
    private Text difficultyText;

    [SerializeField]
    private Button easyBtn;

    [SerializeField]
    private Button normalBtn;

    [SerializeField]
    private Button hardBtn;

    private int difficulty = 0;

    private void Awake()
    {
        Time.timeScale = 1;

        this.difficulty = PlayerPrefs.GetInt("Difficulty", difficulty);
        this.OnSetDifficulty(this.difficulty);
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene("Level Scene");
    }

    public void OnSetDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                this.ResetButtons();
                this.easyBtn.interactable = false;
                difficultyText.text = GameConstants.DifficultyPrefix + "Easy";
                PlayerPrefs.SetInt("Difficulty", level);
                break;
            case 1:
                this.ResetButtons();
                this.normalBtn.interactable = false;
                difficultyText.text = GameConstants.DifficultyPrefix + "Normal";
                PlayerPrefs.SetInt("Difficulty", level);
                break;
            case 2:
                this.ResetButtons();
                this.hardBtn.interactable = false;
                difficultyText.text = GameConstants.DifficultyPrefix + "Hard";
                PlayerPrefs.SetInt("Difficulty", level);
                break;
        }
    }

    private void ResetButtons()
    {
        this.easyBtn.interactable = true;
        this.normalBtn.interactable = true;
        this.hardBtn.interactable = true;
    }

    public void OnPanelOpened()
    {
        AudioManager.Instance.Play(AudioClipName.PanelOpened);
    }

    public void OnPanelClosed()
    {
        AudioManager.Instance.Play(AudioClipName.PanelClosed);
    }

    public void OnButtonClicked()
    {
        AudioManager.Instance.Play(AudioClipName.ButtonClick);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
