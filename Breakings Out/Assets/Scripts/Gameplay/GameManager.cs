using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Image InstructionImg;
    [SerializeField]
    private Image WeaponTypesImg;
    [SerializeField]
    private Image BlockTypesImg;
    [SerializeField]
    private Image ControlsImg;

    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button QuitBtn;
    [SerializeField]
    private Button HowtoplayBtn;
    [SerializeField]
    private Button ControlsBtn;

    void Start () {
        InstructionImg.gameObject.SetActive(false);
        WeaponTypesImg.gameObject.SetActive(false);
        BlockTypesImg.gameObject.SetActive(false);
        ControlsImg.gameObject.SetActive(false);
    }
	
	void Update () {
		
	}

    public void QuitButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        Application.Quit();
    }

    public void StartGame()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.PlayerSound[Random.Range(0,2)]);
        SceneManager.LoadScene("Scene0");
    }

    public void HowtoplayButton()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);

        InstructionImg.gameObject.SetActive(true);

        StartBtn.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);
        HowtoplayBtn.gameObject.SetActive(false);
        ControlsBtn.gameObject.SetActive(false);
    }

    public void GotItBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick);

        InstructionImg.gameObject.SetActive(false);
        WeaponTypesImg.gameObject.SetActive(false);
        BlockTypesImg.gameObject.SetActive(false);
        ControlsImg.gameObject.SetActive(false);

        StartBtn.gameObject.SetActive(true);
        QuitBtn.gameObject.SetActive(true);
        HowtoplayBtn.gameObject.SetActive(true);
        ControlsBtn.gameObject.SetActive(true);
    }

    public void WepTypBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);

        InstructionImg.gameObject.SetActive(false);

        WeaponTypesImg.gameObject.SetActive(true);
    }

    public void BlkTypBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);

        InstructionImg.gameObject.SetActive(false);

        BlockTypesImg.gameObject.SetActive(true);
    }

    public void backBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);

        InstructionImg.gameObject.SetActive(true);
        WeaponTypesImg.gameObject.SetActive(false);
        BlockTypesImg.gameObject.SetActive(false);
    }

    public void GameControlsBtn()
    {
        SoundManager.instance.AudSource.PlayOneShot(SoundManager.instance.BtnClick2);

        ControlsImg.gameObject.SetActive(true);

        StartBtn.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);
        HowtoplayBtn.gameObject.SetActive(false);
        ControlsBtn.gameObject.SetActive(false);
    }
}
