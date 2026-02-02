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
    public StatSheet statSheet;

    public List<AgentResource> allResources = new List<AgentResource>();

    public bool isAlive;

    public AgentAlignment alignment;

    //List of Status Effects


    public Agent(GridSystem grid, GridCell spawnPoint, AgentRace race, AgentClass agentClass, AgentAlignment agentAlignment) 
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


        alignment = agentAlignment;
        statSheet = new StatSheet(10,10,10,10);
        allResources.Add(new Health());
        allResources.Add(new ActionPoint());
        isAlive = true;
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

    //public T GetResource<T>() where T : AgentResource
    //{
    //    return resource
    //}
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
