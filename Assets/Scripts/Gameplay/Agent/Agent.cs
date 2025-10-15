using UnityEngine;
using System.Collections.Generic;
public class Agent
{
    //Grid Information
    public GridCell gridPos;

    //Agent Components
    public List<AgentClass> agentClasses = new List<AgentClass>();
    public AgentRace agentRace;
    public AStarPathfinding pathfinding = new AStarPathfinding();

    //Agent Information
    public int AgentLevel = 1;

    public List<AgentAction> allActions = new List<AgentAction>();
    public List<AgentTrait> allTraits = new List<AgentTrait>();

    AgentAlignment alignment;
    //List of Status Effects


    public Agent(GridSystem grid, GridCell spawnPoint, AgentRace race, AgentClass agentClass) // Add these AgentAlignment agentAlignment,
    {
        gridPos = spawnPoint;
        grid.gridArray[spawnPoint.x, spawnPoint.y].EntityOnTile = EntityType.Agent;



        //Agent Information
        agentRace = race;
        agentClasses.Add(agentClass);
        
        foreach (AgentAction action in agentClass.ClassActions)
        {
            allActions.Add(action);
        }

        foreach (AgentTrait trait in agentRace.RaceTraits)
        {
            allTraits.Add(trait);
        }
    }


    //Movement
    public void MoveTo(GridSystem grid, GridCell Destination)
    {
        

        if (grid.gridArray[Destination.x, Destination.y].walkable && Destination != null)
        {
            SetEntityOnTile(grid, false);
            gridPos = Destination;
            SetEntityOnTile(grid, true);
        }

        //Debug.Log("The Player is at Grid Cell: (" + gridPos.x + ", " + gridPos.y + ")");
    }

    public void CombatMoveTo(GridSystem grid, GridCell Destination)
    {
        List<GridCell> path = pathfinding.Pathfinding(gridPos, Destination, grid);

        foreach (GridCell cell in path)
        {
            MoveTo(grid, cell);
        }


    }


    //Setting Information
    public int GetAgentLevel(List<AgentClass> classes)
    {
        int level = 0;

        foreach (AgentClass aClass in classes)
        {
            level += aClass.ClassLevel;
        }

        return level;
    }

    private void SetEntityOnTile(GridSystem grid, bool OnTile)
    {
        if (OnTile)
        {
            grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.Agent;
        }
        else if (!OnTile)
        {
            grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.None;
        }

    }

}
