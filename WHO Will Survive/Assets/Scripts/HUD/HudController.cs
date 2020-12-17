using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UiObjects = new List<GameObject>();

    [SerializeField]
    private Text recoveredCasesText;

    [SerializeField]
    private Text suspectedCasesText;

    [SerializeField]
    private Text gameStateText;

    [SerializeField]
    private RectTransform sanetizationIndicator;

    private bool isMenuActive = false;

    private void Awake()
    {
        EventsManager.Instance.OnSanetizationAvailable += this.OnSanetizationAvailable;
        EventsManager.Instance.OnSanetizationActivated += this.OnSanetizationActivated;
        EventsManager.Instance.OnGameEnded += this.OnGameEnded;
        EventsManager.Instance.OnUnitDestroyed += this.OnUnitDestroyed;

        this.isMenuActive = false;
    }

    private void Update()
    {
        this.HandleEscapeButtonClicked();
        this.suspectedCasesText.text = GameConstants.SuspectedCasesPrefix + GameManager.Instance.SuspectedCases.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.UiObjects[1].SetActive(false);
        this.UiObjects[2].SetActive(false);
        this.recoveredCasesText.text = GameConstants.RecoveredCasesPrefix + GameManager.Instance.RecoveredCases.ToString();
        this.suspectedCasesText.text = GameConstants.SuspectedCasesPrefix + GameManager.Instance.SuspectedCases.ToString();
    }

    private void HandleEscapeButtonClicked()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.OnButtonClicked();
            if (this.isMenuActive)
            {
                this.OnPanelClosed();
            }
            else
            {
                this.OnPanelOpened();
            }

            this.UiObjects[1].SetActive(!this.isMenuActive);
            this.UiObjects[3].GetComponent<Button>().interactable = this.isMenuActive;
            this.OnMenuOrResumeClicked(this.isMenuActive);
        }
    }

    private void OnSanetizationAvailable()
    {
        this.sanetizationIndicator.gameObject.SetActive(true);
    }

    private void OnSanetizationActivated()
    {
        this.sanetizationIndicator.gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles unit destruction trigger
    /// </summary>
    /// <param name=""></param>
    private void OnUnitDestroyed(int cases)
    {
        GameManager.Instance.RecoveredCases += cases;
        this.recoveredCasesText.text = GameConstants.RecoveredCasesPrefix + GameManager.Instance.RecoveredCases.ToString();
    }

    private void OnGameEnded(bool state)
    {
        this.CloseAllUiObjects();

        this.UiObjects[2].SetActive(true);
        this.UiObjects[2].GetComponent<EndGamePanelController>()?.InitializePanel();

        if (state)
        {
            this.gameStateText.text = GameConstants.WinPrefix;
        }
        else
        {
            this.gameStateText.text = GameConstants.LosePrefix;
        }
    }

    public void OnMenuOrResumeClicked(bool state)
    {
        if (state)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        this.isMenuActive = !this.isMenuActive;
    }

    public void OnPlayAgainClicked()
    {
        SceneManager.LoadScene(GameConstants.LevelScenePrefix);
    }

    public void OnBackToMainmenuClicked()
    {
        SceneManager.LoadScene(GameConstants.MainScenePrefix);
    }

    public void OnButtonClicked()
    {
        AudioManager.Instance.Play(AudioClipName.ButtonClick);
    }

    public void OnPanelOpened()
    {
        AudioManager.Instance.Play(AudioClipName.PanelOpened);
    }

    public void OnPanelClosed()
    {
        AudioManager.Instance.Play(AudioClipName.PanelClosed);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    private void CloseAllUiObjects()
    {
        this.recoveredCasesText.gameObject.SetActive(false);
        this.suspectedCasesText.gameObject.SetActive(false);

        foreach (var uiObject in this.UiObjects)
        {
            uiObject.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if(EventsManager.Instance != null)
        {
            EventsManager.Instance.OnUnitDestroyed -= OnUnitDestroyed;
        }
    }
}
