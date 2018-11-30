using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineCreator : Singleton<LineCreator> {

    #region Fields
    [SerializeField]
    private LineBtn lineBtnPressed ;
    [SerializeField]
    private Text RemainLinesTxt;

    Line activeLine;
    List<Line> LineList = new List<Line>();
    #endregion

    void Start()
    {
        RemainLinesTxt.text = "Lines Remaining : " + GameManager.Instance.TotalLines;
    }

    void Update()
    {
        RemainLinesTxt.text = "Lines Remaining : " + GameManager.Instance.RemainingLines;
        if (Input.GetMouseButtonDown(0) && LineList.Count < GameManager.Instance.TotalLines && !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject LineGo = Instantiate(lineBtnPressed.LineObject);
            activeLine = LineGo.GetComponent<Line>();
            RegisterLine(activeLine);
            GameManager.Instance.subtractLines();
            RemainLinesTxt.text = "Lines Remaining : " + GameManager.Instance.RemainingLines;
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null && LineList.Count <= GameManager.Instance.TotalLines)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

    #region Functions
    public void selectedLine(LineBtn lineBtn)
    {
        lineBtnPressed = lineBtn;
    }

    public void RegisterLine(Line line)
    {
        LineList.Add(line);
    }

    public void DestroyAllLines()
    {
        foreach (Line line in LineList)
        {
            Destroy(line.gameObject);
        }
        LineList.Clear();
        GameManager.Instance.RemainingLines = GameManager.Instance.TotalLines;
    }
    #endregion
}
