using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls UI logic of main menu scene
/// </summary>
public class MainUIController : MonoBehaviour
{
    private AudioManager AudioManager => AudioManager.Instance;

    public void OnButtonClicked()
    {
        this.AudioManager.PlaySoundEffect(AudioKey.ButtonClick);
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene(GameConstants.LevelSceneName);
    }

    public void OnExitClosed()
    {
        Application.Quit();
    }
}
