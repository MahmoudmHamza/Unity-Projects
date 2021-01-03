using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    [SerializeField]
    private List<Button> levelButtonsList;

    private LevelManager levelManager => LevelManager.Instance;

    void Start()
    {
        this.levelManager.OnResetProgress += this.ResetProgress;
        this.UnlockLevelProgress();
    }

    private void UnlockLevelProgress()
    {
        var levelsList = this.levelManager.GetObtainedStoneList();

        if (!levelsList.Contains(InfinityStoneType.Mind))
        {
            return;
        }

        foreach(var button in this.levelButtonsList)
        {
            button.interactable = true;
        }
    }

    private void ResetProgress()
    {
        foreach (var button in this.levelButtonsList)
        {
            button.interactable = false;
        }
    }

    private void OnDestroy()
    {
        if(this.levelManager != null)
        {
            this.levelManager.OnResetProgress -= this.ResetProgress;
        }
    }
}
