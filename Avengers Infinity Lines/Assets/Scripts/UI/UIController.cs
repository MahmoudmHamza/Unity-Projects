using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private RectTransform gameStatusPanel;

    [SerializeField]
    private RectTransform stoneInfoPanel;

    [SerializeField]
    private RectTransform levelInfoPanel;

    [SerializeField]
    private RectTransform stonesPanel;

    [SerializeField]
    private Text stoneInfoText;

    [SerializeField]
    private Text statusDescriptionText;

    [SerializeField]
    private Text planetText;

    [SerializeField]
    private Text lineText;

    [SerializeField]
    private Text statusButtonText;

    [SerializeField]
    private Text remainLinesText;

    [SerializeField]
    private Text lineLengthText;

    [SerializeField]
    private Text selectedStoneHintText;

    private LevelManager LevelManager => LevelManager.Instance;

    private AudioManager AudioManager => AudioManager.Instance;

    private void Start()
    {
        this.ToggleGameStatusPanel(false);
    }

    public void InitializeGamePlayUI(Line line, Planet planet)
    {
        this.planetText.text = planet.ToString();
        this.lineText.text = line.Name;
    }

    public void ToggleGameStatusPanel(bool state, Level level = Level.Level0, GameStatus status = GameStatus.OnGoing)
    {
        this.gameStatusPanel.gameObject.SetActive(state);

        switch (status)
        {
            case GameStatus.Win:
                this.statusDescriptionText.text = "Level Completed";
                this.statusButtonText.text = "Next Level";
                this.ShowObtainedStoneInfo(level);
                break;

            case GameStatus.Lose:
                this.statusDescriptionText.text = "Level Failed";
                this.statusButtonText.text = "Try Again";
                break;

            default:
                break;
        }
    }

    public void UpdateLineCount(int count)
    {
        this.remainLinesText.text = "Lines: " + count.ToString();
    }

    public void UpdateLineName(Line line)
    {
        this.lineText.text = line.Name;
    }

    public void UpdateLineLength(int length)
    {
        var lineLength = length < 0 ? 0 : length;
        this.lineLengthText.text = "Length: " + lineLength.ToString();
    }

    public void ShowSelectedStoneHint(InfinityStoneType stoneType, bool autoHide = false)
    {
        switch (stoneType)
        {
            case InfinityStoneType.Mind:
                this.selectedStoneHintText.text = "<color=yellow>Line's length increased!</color>";
                break;
            case InfinityStoneType.Space:
                this.selectedStoneHintText.text = "<color=blue>Avenger transported!</color>";
                break;
            case InfinityStoneType.Power:
                this.selectedStoneHintText.text = "<color=purple>Hit an enemy to destroy it!</color>";
                break;
            case InfinityStoneType.Time:
                this.selectedStoneHintText.text = "<color=lime>Enemies slowed down!</color>";
                break;
            case InfinityStoneType.Reality:
                this.selectedStoneHintText.text = "<color=red>Choose another line type...</color>";
                break;
            case InfinityStoneType.Soul:
                this.selectedStoneHintText.text = "<color=orange>Destroy half of thanos troops...</color>";
                break;
        }

        this.selectedStoneHintText.gameObject.SetActive(true);

        if (autoHide)
        {
            this.StartCoroutine(this.WaitThenHideHint());
        }
    }

    public void HideHintPanel()
    {
        this.selectedStoneHintText.gameObject.SetActive(false);
    }

    public IEnumerator WaitThenHideHint()
    {
        yield return new WaitForSeconds(3f);
        this.HideHintPanel();
    }

    private void ShowObtainedStoneInfo(Level levelName)
    {
        var stoneInfo = this.LevelManager.GetDescriptionByLevel(levelName);
        this.stoneInfoText.text = stoneInfo;
        this.stoneInfoPanel.gameObject.SetActive(true);
    }

    public void OnButtonClicked()
    {
        this.AudioManager.PlaySoundEffect(AudioKey.ButtonClick);
    }

    public void OnLevelButtonClicked(string levelName)
    {
        if(!Enum.TryParse<Level>(levelName, out var levelIndex))
        {
            return;
        }

        this.LevelManager.LoadLevel(levelIndex);
    }

    public void HideLevelUI()
    {
        this.levelInfoPanel.gameObject.SetActive(false);
        this.stonesPanel.gameObject.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Game Menu");
    }
}
