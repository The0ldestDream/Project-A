using UnityEngine;
using System;
using System.Collections.Generic;
public class Agent : IDamageable
{
    //Grid Information
    public GridCell gridPos;

    //Agent Components
    
    public AgentController controller;

    //Agent Information
    public int AgentLevel = 1;
    public List<AgentClass> agentClasses = new List<AgentClass>();
    public AgentRace agentRace;
    public List<AgentAction> allActions = new List<AgentAction>();
    public StatSheet statSheet;
    public List<AgentResource> allResources = new List<AgentResource>();

    public Direction agentFacingDirection = Direction.None;

    public bool isAlive;

    public AgentAlignment alignment;

    //List of Status Effects


    public Agent(GridSystem grid, GridCell spawnPoint, AgentRace race, AgentClass agentClass, AgentAlignment agentAlignment) 
    {
        gridPos = spawnPoint;
        SetEntityOnTile(grid, true);

        //Agent Information
        agentRace = race;
        agentClasses.Add(agentClass);

        alignment = agentAlignment;
        statSheet = new StatSheet(10,10,10,10);
        allResources.Add(new Health(statSheet));
        allResources.Add(new ActionPoint());
        isAlive = true;

        allActions.Add(new Move(1, 1));
        allActions.Add(new Interact(1, 1));
        agentClass.GiveActions(this);
        agentRace.GiveActionsandTraits(this);
    }




    //Movement
    public void MoveTo(GridSystem grid, GridCell Destination)
    {

        if (grid.gridArray[Destination.x, Destination.y].walkable && Destination != null)
        {
            FindDirection(gridPos, Destination);
            SetEntityOnTile(grid, false);
            gridPos = Destination;
            SetEntityOnTile(grid, true);
        }

        //Debug.Log("The Player is at Grid Cell: (" + gridPos.x + ", " + gridPos.y + ")");
    }

    //Getting and Setting Information
    public int GetAgentLevel(List<AgentClass> classes)
    {
        int level = 0;

        foreach (AgentClass aClass in classes)
        {
            level += aClass.ClassLevel;
        }

        return level;
    }

    public void DealDamage(int DamageTaken)
    {
        AgentResource health = allResources.Find(x => x.ResourceName == "Health");
        Debug.Log("Agent health is at: " + health.currentAmount);

        health.AdjustValue(-DamageTaken);
        Debug.Log("Agent health has dropped to: " + health.currentAmount);

        //Could use a AgentDamage Event later on to tell listeners that this agent has been damaged


        if (health.currentAmount <= 0)
        {
            AgentDeath();
        }
    }

    private void AgentDeath()
    {
        OnDeath?.Invoke(this);
    }



    private void SetEntityOnTile(GridSystem grid, bool OnTile)
    {
        if (OnTile)
        {
            grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.Agent;
            grid.gridArray[gridPos.x, gridPos.y].AgentOnTile = this;
            grid.gridArray[gridPos.x, gridPos.y].damageable = this;
        }
        else if (!OnTile)
        {
            grid.gridArray[gridPos.x, gridPos.y].EntityOnTile = EntityType.None;
            grid.gridArray[gridPos.x, gridPos.y].AgentOnTile = null;
            grid.gridArray[gridPos.x, gridPos.y].damageable = null;
        }

    }


    public void FindDirection(GridCell A, GridCell B)
    {
        int dx = B.x - A.x;
        int dy = B.y - A.y;

        if (Mathf.Abs(dx) > Mathf.Abs(dy))
        {
            if (dx > 0)
            {
                agentFacingDirection = Direction.Right;
            }
            else
            {
                agentFacingDirection = Direction.Left;
            }
        }
        else if (Mathf.Abs(dy) > Mathf.Abs(dx))
        {
            if (dy > 0)
            {
                agentFacingDirection = Direction.Up;
            }
            else
            {
                agentFacingDirection = Direction.Down;
            }
        }
    }

    //Events
    public event Action<Agent> OnDeath;

}
