using UnityEngine;
using System.Collections.Generic;

public class GridCell
{
    //Grid cell Size
    public int cellheight;
    public int cellwidth;

    //Grid Cell Position
    public Vector2 worldPos;
    public int x, y;
    public List<GridCell> neighbours = new List<GridCell>();

    //Grid Information
    public TileType TypeOfTile = TileType.emptyTile;
    public EntityType EntityOnTile = EntityType.None;
    public ObjectType ObjectOnTile = ObjectType.None;
    public Agent AgentOnTile;

    public IInteractable interactable = null;
    public IDamageable damageable = null;

    public int cost;
    public bool walkable = true;

    public GridCell(int xPos, int yPos)
    {
        x = xPos;
        y = yPos;

        cellheight = 1;
        cellwidth = 1;

        cost = 1;
    }

}
