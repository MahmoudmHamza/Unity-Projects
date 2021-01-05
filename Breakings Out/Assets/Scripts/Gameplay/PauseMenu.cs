using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Text audioText;

    [SerializeField]
    private AudioSource audioSource;

    private SoundManager SoundManager => SoundManager.instance;

    private bool isAudioEnable = true;

    public void OnButtonClicked()
    {
        this.SoundManager.AudSource.PlayOneShot(this.SoundManager.BtnClick2);
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
    }

    public void OnAudioButtonClicked()
    {
        this.isAudioEnable = !this.isAudioEnable;
        this.audioSource.enabled = this.isAudioEnable;
        this.audioText.text = this.isAudioEnable ? "Audio : ON" : "Audio : OFF";
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnRestartGameClicked()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.PlayerSound[Random.Range(0, 2)]);
        SceneManager.LoadScene("Level Scene");
    }
}
