using System;
using UnityEngine;
using UnityEngine.UI;

public class StoneInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Text infoText;

    private LevelManager LevelManager => LevelManager.Instance;

    private void OnEnable()
    {
        this.infoText.text = "<size=25px><color=orange>The Infinity Stones are tied to different aspects of the universe.</color></size>\nChoose a stone to show its info...";
    }

    public void OnStoneClicked(string stoneType)
    {
        if(!Enum.TryParse<InfinityStoneType>(stoneType, out var stone))
        {
            return;
        }

        var stoneInfo = this.LevelManager.GetStoneDescription(stone);
        this.infoText.text = stoneInfo;
    }
}
