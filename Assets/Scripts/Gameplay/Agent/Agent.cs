using UnityEngine;

public class Agent
{
    //Grid Information
    public GridCell gridPos;

    //Agent Components

    //Agent Information

    public Agent(GridSystem grid, GridCell spawnPoint)
    {
        gridPos = spawnPoint;
        grid.gridArray[spawnPoint.x, spawnPoint.y].EntityOnTile = EntityType.Agent;
    }




    public void MoveTo(GridSystem grid, GridCell Destination)
    {
        

        if (grid.gridArray[Destination.x, Destination.y].walkable && Destination != null)
        {
            SetEntityNoneOnTile(grid);
            gridPos = Destination;
            SetEntityOnTile(grid);
        }

        //Debug.Log("The Player is at Grid Cell: (" + gridPos.x + ", " + gridPos.y + ")");
    }



    private void SetEntityOnTile(GridSystem grid)
    {

        grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.Agent;

    }

    private void SetEntityNoneOnTile(GridSystem grid)
    {

        grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.None;

    }

}
