using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Text infoText;

    private Dictionary<LineType, string> lineDescriptionByType = new Dictionary<LineType, string>
    {
        {LineType.Normal, GameConstants.NormalLineDescriptionKey },
        {LineType.Boost, GameConstants.BoostLineDescriptionKey },
        {LineType.Bounce, GameConstants.BounceLineDescriptionKey }
    };

    private void OnEnable()
    {
        this.infoText.text = "Choose a line to show its info...";
    }

    public void OnLineClicked(string lineKey)
    {
        if (!Enum.TryParse<LineType>(lineKey, out var lineType))
        {
            return;
        }

        var lineInfo = this.lineDescriptionByType[lineType];
        this.infoText.text = lineInfo;
    }
}
