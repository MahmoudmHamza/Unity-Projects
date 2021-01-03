using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private string lineName;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private EdgeCollider2D edgeCollider;

    public string Name => this.lineName;

    private GameManager GameManager => GameManager.Instance;

    private int maxLineLength;

    private List<Vector2> points;

    public void InitializeLine(int maxLength)
    {
        this.maxLineLength = maxLength;
        this.GameManager.UIController.UpdateLineLength(this.maxLineLength);
    }

    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            this.points = new List<Vector2>();
            this.SetPoints(mousePos);
            return;
        }

        if(this.points.Count >= this.maxLineLength)
        {
            return;
        }

        if (Vector2.Distance(this.points.Last(), mousePos) > 0.1f)
        {
            this.SetPoints(mousePos);
        }
    }

    void SetPoints(Vector2 point)
    {
        this.points.Add(point);
        this.GameManager.UIController.UpdateLineLength(this.maxLineLength - this.points.Count);

        this.lineRenderer.positionCount = this.points.Count;
        this.lineRenderer.SetPosition(this.points.Count - 1, point);

        if (this.points.Count > 1)
        { 
            this.edgeCollider.points = this.points.ToArray();
        }
    }
}
