using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private Image pauseMenu;
    [SerializeField]
    private Text audioText;

    [SerializeField]
    private AudioSource audioSource;

    private bool buttonPressed = true;
    private bool gamePaused;

    // Use this for initialization
    void Start () {
        pauseMenu.gameObject.SetActive(false);
        gamePaused = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void PauseMenuBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);
        if (!gamePaused)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            gamePaused = true;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            gamePaused = false;
        }
    }

    public void ResumeButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void AudioButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        if (buttonPressed)
        {
            audioSource.enabled = false;
            audioText.text = "Audio : OFF";
            buttonPressed = false;
        }
        else
        {
            audioSource.enabled = true;
            audioText.text = "Audio : ON";
            buttonPressed = true;
        }
    }

    public void HomeButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        SceneManager.LoadScene("Scene1");
    }

    public void QuitButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        Application.Quit();
    }

    public void RestartGame()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.PlayerSound[Random.Range(0, 2)]);
        SceneManager.LoadScene("Scene0");
    }
}
