using UnityEngine;
using System.Collections.Generic;

public class Corridor
{

    public Room roomA;
    public Room roomB;

    public RectInt hSeg;
    public RectInt vSeg;

    public List<Vector2Int> cTiles = new List<Vector2Int>();


    public Vector2 getCenter(RectInt segment)
    {
        float centerX = (segment.x + (segment.width / 2f));
        float centerY = (segment.y + (segment.height / 2f));
        return new Vector2(centerX, centerY);
    }

    public Vector2 getSectorSize(RectInt segment)
    {
        return segment.size;
    }
}
