using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    #region Fields
    public LineRenderer lineRend;
    public EdgeCollider2D egdeCol;

    List<Vector2> points;
    #endregion

    #region Functions
    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoints(mousePos);
            return;
        }

        if(Vector2.Distance(points.Last(), mousePos) > 0.1f)
        {
            SetPoints(mousePos);
        }
    }

    void SetPoints(Vector2 point)
    {
        points.Add(point);

        lineRend.positionCount = points.Count;
        lineRend.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
        { 
        egdeCol.points = points.ToArray();
         
        }
    }
    #endregion
}
