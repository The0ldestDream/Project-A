using UnityEngine;

public class Target
{
    public GridCell tile;
    public Agent agent;


    public Target(GridCell targetCell, Agent AgentOnTile = null)
    {

        tile = targetCell;
        agent = targetCell.AgentOnTile;

    }

}
