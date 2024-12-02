using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Line : MonoBehaviour
{

    public LineRenderer lineRender;
    List<Vector2> points;
    // Start is called before the first frame update
    
    public void UpdateLine(Vector2 position)
    {
        if(points == null)
        {
            points = new List<Vector2>();
            setPoint(position);
            return;
        }
        if(Vector2.Distance(points.Last(), position ) > .1f)
        {
            setPoint(position);
        }

    }
    void setPoint(Vector2 point)
    {
        points.Add(point);

        lineRender.positionCount = points.Count;
        lineRender.SetPosition(points.Count - 1, point);

    }

    public List<Vector2> GetPoints()
    {
        return points;
    }


}
