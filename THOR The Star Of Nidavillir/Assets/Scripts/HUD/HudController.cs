using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private Text gameStateText;

    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private List<GameObject> HudObjects = new List<GameObject>();

    private void Awake()
    {
        EventsManager.Instance.OnPlayerDestroyed += OnPlayerDestroyed;
        EventsManager.Instance.OnWeaponForged += OnWeaponForged;
        EventsManager.Instance.OnStarMelted += OnStarMelted;
        EventsManager.Instance.OnStarFroze += OnStarFroze;
    }

    private void OnPlayerDestroyed()
    {
        CloseAllUI();
        HudObjects[3].SetActive(true);
        menuButton.SetActive(false);
        gameStateText.text = GameConstants.LosePrefix;
        AudioManager.Instance.Play(AudioClipName.Lose);
    }

    private void OnWeaponForged()
    {
        CloseAllUI();
        HudObjects[3].SetActive(true);
        menuButton.SetActive(false);
        gameStateText.text = GameConstants.WinPrefix;
        AudioManager.Instance.Play(AudioClipName.Win);
    }

    private void CloseAllUI()
    {
        foreach(var obj in HudObjects)
        {
            obj.SetActive(false);
        }
    }

    private void OnStarMelted()
    {
        HudObjects[4].SetActive(true);
        HudObjects[4].GetComponent<StormBreakerController>().Initialize();
    }

    private void OnStarFroze()
    {
        HudObjects[4].GetComponent<StormBreakerController>().ResetBarValues();
        HudObjects[4].SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void OnPlayAgainButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Scene");
    }

    public void OnMenuButtonClicked()
    {
        HudObjects[5].SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1f;
        HudObjects[5].SetActive(false);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnButtonClicked()
    {
        AudioManager.Instance.Play(AudioClipName.ButtonClick);
    }
}
