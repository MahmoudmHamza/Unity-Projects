using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StonesController : MonoBehaviour
{
    [SerializeField]
    private Stone stonePrefab;

    [SerializeField]
    private GameObject thanosPrefab;

    [SerializeField]
    private Transform stoneSpawnPoint;

    [SerializeField]
    private RectTransform stonesPanel;

    [SerializeField]
    private RectTransform linesPanel;

    [SerializeField]
    private Button guantletButton;

    [SerializeField]
    private List<StoneButtonEntry> stoneButtons;

    public InfinityStoneType StoneType { get; set; }

    private GameManager GameManager => GameManager.Instance;

    private LineCreator LineCreator => LineCreator.Instance;

    void Start()
    {
        this.InitializeButtons();
        this.SpawnLevelStone();
    }

    private void SpawnLevelStone()
    {
        if (this.StoneType == InfinityStoneType.Guantlet)
        {
            var thanosObject = Instantiate(this.thanosPrefab);
            thanosObject.transform.position = this.stoneSpawnPoint.position;
            return;
        }

        var stoneObject = Instantiate(this.stonePrefab) as Stone;
        stoneObject.transform.position = this.stoneSpawnPoint.position;
        stoneObject.InitializeStone(this.StoneType);
    }

    private void InitializeButtons()
    {
        this.DisableButtons();

        var stonesList = LevelManager.Instance.GetObtainedStoneList();

        foreach(var stone in stonesList)
        {
            foreach (var buttonEntry in this.stoneButtons)
            {
                if(buttonEntry.StoneType == stone)
                {
                    buttonEntry.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    private void DisableButtons()
    {
        foreach (var buttonEntry in this.stoneButtons)
        {
            buttonEntry.GetComponent<Button>().interactable = false;
        }
    }

    public void OnStoneClicked(string stoneType)
    {
        if (this.GameManager.IsStoneUsed)
        {
            Debug.Log("Infinity stone already used");
            return;
        }

        if(!Enum.TryParse<InfinityStoneType>(stoneType, out var stone))
        {
            return;
        }

        this.ApplyStoneEffect(stone);
        this.GameManager.IsStoneUsed = true;
    }

    private void ApplyStoneEffect(InfinityStoneType stoneType)
    {
        switch (stoneType)
        {
            case InfinityStoneType.Mind:
                this.LineCreator.OnMindStoneUsed();
                break;
            case InfinityStoneType.Time:
                this.GameManager.EnemyController.OnTimeStoneUsed();
                break;
            case InfinityStoneType.Soul:
                this.GameManager.OnSoulStoneUsed();
                break;
            case InfinityStoneType.Reality:
                this.OnRealityStoneUsed();
                break;
            case InfinityStoneType.Power:
                this.GameManager.OnPowerStoneUsed();
                break;
            case InfinityStoneType.Space:
                this.GameManager.OnSpaceStoneUsed();
                break;
        }
    }

    public void OnRealityStoneUsed()
    {
        this.linesPanel.gameObject.SetActive(true);
        this.guantletButton.interactable = false;

        this.GameManager.UIController.ShowSelectedStoneHint(InfinityStoneType.Reality);
    }

    public void OnLineButtonSelected(Line line)
    {
        if (line.Name == this.LineCreator.LevelLine.Name)
        {
            return;
        }

        this.LineCreator.LevelLine = line;
        this.GameManager.UIController.UpdateLineName(line);

        this.GameManager.IsSelectingStone = false;
        this.linesPanel.gameObject.SetActive(false);
        this.GameManager.UIController.HideHintPanel();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnResetStoneUsed()
    {
        this.GameManager.IsStoneUsed = false;
    }

    public void OnSelectStoneClicked()
    {
        this.GameManager.IsSelectingStone = !this.GameManager.IsSelectingStone;
        this.stonesPanel.gameObject.SetActive(this.GameManager.IsSelectingStone);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
