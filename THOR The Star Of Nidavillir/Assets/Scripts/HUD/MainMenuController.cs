using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        AudioManager.Instance.Play(AudioClipName.ButtonClick);
    }

    public void OnNewGameClicked()
    {
        OnButtonClicked();
        SceneManager.LoadScene("Main Scene");
    }

    public void OnExitClicked()
    {
        OnButtonClicked();
        Application.Quit();
    }
}
