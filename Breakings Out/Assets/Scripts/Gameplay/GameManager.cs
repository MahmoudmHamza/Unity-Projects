using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SoundManager SoundManager => SoundManager.instance;

    public void OnStartGameClicked()
    {
        this.SoundManager.AudSource.PlayOneShot(this.SoundManager.BtnClick);
        this.SoundManager.AudSource.PlayOneShot(this.SoundManager.PlayerSound[Random.Range(0,2)]);
        SceneManager.LoadScene("Level Scene");
    }

    public void OnButtonClicked()
    {
        this.SoundManager.AudSource.PlayOneShot(this.SoundManager.BtnClick2);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
