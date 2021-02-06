using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls UI logic of level scene
/// </summary>
public class LevelUIController : MonoBehaviour
{
    [SerializeField]
    private RectTransform gameEndPanel;

    private GameManager GameManager => GameManager.Instance;

    private AudioManager AudioManager => AudioManager.Instance;

    private void Awake()
    {
        this.GameManager.OnGameEnded += this.OnGameEnded;
    }

    private void OnGameEnded(GameStatus status)
    {
        var gameEndText = this.gameEndPanel.GetComponentInChildren<Text>();

        switch (status)
        {
            case GameStatus.Win:
                this.AudioManager.PlaySoundEffect(AudioKey.Win);
                gameEndText.text = GameConstants.VictoryKey;
                break;

            case GameStatus.Lose:
                this.AudioManager.PlaySoundEffect(AudioKey.Lose);
                gameEndText.text = GameConstants.DefeatKey;
                break;
        }

        this.gameEndPanel.gameObject.SetActive(true);
    }

    public void OnButtonClicked()
    {
        this.AudioManager.PlaySoundEffect(AudioKey.ButtonClick);
    }

    public void OnPauseMenuClicked()
    {
        Time.timeScale = 0;
    }

    public void OnResumeClicked()
    {
        Time.timeScale = 1f;
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameConstants.LevelSceneName);
    }

    public void OnBackClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameConstants.MainSceneName);
    }

    private void OnDestroy()
    {
        if(this.GameManager != null)
        {
            this.GameManager.OnGameEnded += this.OnGameEnded;
        }
    }
}
