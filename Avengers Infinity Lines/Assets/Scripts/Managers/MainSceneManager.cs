using UnityEngine;
using UnityEngine.UI;
using System;

public class MainSceneManager : Singleton<MainSceneManager>
{
    [SerializeField]
    private Image instructionsPanel;

    private LevelManager LevelManager => LevelManager.Instance;

    private AudioManager AudioManager => AudioManager.Instance;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        this.instructionsPanel.gameObject.SetActive(false);
    }

    public void OnLevelSelected(string level)
    {
        if(!Enum.TryParse<Level>(level, out var levelIndex))
        {
            Debug.LogError("Wrong level name");
            return;
        }

        this.LevelManager.LoadLevel(levelIndex);
    }

    public void OnResetClicked()
    {
        this.LevelManager.OnResetProgressClicked();
    }

    public void OnButtonClicked()
    {
        this.AudioManager.PlaySoundEffect(AudioKey.ButtonClick);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
