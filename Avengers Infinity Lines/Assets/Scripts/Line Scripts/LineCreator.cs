using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineCreator : Singleton<LineCreator>
{
    public Line LevelLine { get; set; }

    public int MaxLength { get; set; }

    public int MaxLines { get; set; }

    private GameManager GameManager => GameManager.Instance;

    private List<Line> lineList = new List<Line>();

    private Line activeLine;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        this.GameManager.UIController.UpdateLineCount(this.MaxLines - this.lineList.Count);
        this.GameManager.UIController.UpdateLineLength(this.MaxLength);
    }

    void Update()
    {
        this.HandleMouseInput();
        this.UpdateActiveLine();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && this.lineList.Count < this.MaxLines)
        {
            var lineObject = Instantiate(this.LevelLine.gameObject);
            this.activeLine = lineObject.GetComponent<Line>();
            this.activeLine.InitializeLine(this.MaxLength);
            this.RegisterLine(this.activeLine);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.activeLine = null;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            this.DestroyAllLines();
        }
    }

    private void UpdateActiveLine()
    {
        if (this.activeLine == null)
        {
            return;
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.activeLine.UpdateLine(mousePos);
    }

    public void RegisterLine(Line line)
    {
        this.lineList.Add(line);
        this.GameManager.UIController.UpdateLineCount(this.MaxLines - this.lineList.Count);
    }

    public void DestroyAllLines()
    {
        foreach (Line line in this.lineList)
        {
            Destroy(line.gameObject);
        }

        this.lineList.Clear();
        this.GameManager.UIController.UpdateLineCount(this.MaxLines - this.lineList.Count);
        this.GameManager.UIController.UpdateLineLength(this.MaxLength);
    }

    public void OnMindStoneUsed()
    {
        this.MaxLength += 100;
        this.GameManager.UIController.UpdateLineLength(this.MaxLength);

        this.GameManager.UIController.ShowSelectedStoneHint(InfinityStoneType.Mind, true);
    }
}
