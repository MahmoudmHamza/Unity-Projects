using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingManager : Singleton<StartingManager> {
   
    #region Fields
    [SerializeField]
    private Image InstructionsImage;

    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private Button howToPlayBtn;

    [SerializeField]
    private AudioClip buttonPress;

    private AudioSource audioSource;
    #endregion


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        InstructionsImage.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Functions
    public void StartPressed()
    {
        audioSource.PlayOneShot(buttonPress);
        SceneManager.LoadScene("Game Play");
    }

    public void ExitPressed()
    {
        audioSource.PlayOneShot(buttonPress);
        Application.Quit();
    }

    public void HowToPlayPressed()
    {
        startBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
        howToPlayBtn.gameObject.SetActive(false);
        InstructionsImage.gameObject.SetActive(true);
        audioSource.PlayOneShot(buttonPress);
    }

    public void GotItPressed()
    {
        startBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);
        howToPlayBtn.gameObject.SetActive(true);
        InstructionsImage.gameObject.SetActive(false);
        audioSource.PlayOneShot(buttonPress);
    }
    #endregion
}
