using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    public CombatManager combatManager;
    public GameObject agentPrefab;


    public List<GameObject> EnemyAgents = new List<GameObject>();
    public List<GameObject> FriendlyAgents = new List<GameObject>();


    private List<AgentRace> agentRaces = new List<AgentRace> {new Human()};
    private List<AgentClass> agentClasses = new List<AgentClass> { new Fighter(1, 100) };

    // Update is called once per frame
    void Update()
    {
        
    }


    private GameObject CreateAgent(AgentDescription description, LevelSetup LS, Vector3 spawnPos)
    {
        GridSystem grid = LS.levelGenerator.ourGrid;

        //based on what is passed into the create agent we need to create what is described from it


        AgentRace Race = null;
        switch (description.Race)
        {
            case RaceType.Human:
                Race = new Human();
                break;
        }

        AgentClass Class = null;
        switch (description.Class)
        {
            case ClassType.Fighter:
                Class = new Fighter(description.ClassLevel, description.ClassLevel * 100); // Change MaxExp
                break;
            case ClassType.Sorcerer:
                Class = new Sorcerer(description.ClassLevel, description.ClassLevel * 100);
                break;
            case ClassType.Rogue:
                Class = new Rogue(description.ClassLevel, description.ClassLevel * 100);
                break;

        }

        Agent newAgent = new Agent(grid,
            grid.gridArray[(int)spawnPos.x, (int)spawnPos.y],
            Race,
            Class,
            description.Alignment);




        //Workflow for spawning an agent
        GameObject agent = Instantiate(agentPrefab, spawnPos, Quaternion.identity);
        AgentController AC = agent.GetComponent<AgentController>();
        AC.Init(newAgent);
        AC.pathfinding = combatManager.gameManager.pathfinding;
        AIController AIC = new AIController(newAgent);
        AC.AIC = AIC;

        if (newAgent.alignment == AgentAlignment.Friendly)
        {
            FriendlyAgents.Add(agent);
        }
        else if (newAgent.alignment == AgentAlignment.Enemy)
        {
            EnemyAgents.Add(agent);
        }
        else
        {
            //For neutral agents, I don't know if i will have any for a while
        }

        return agent;
    }


    public void SpawnAgent(Room CombatRoom, AgentDescription agentDescription)
    {
        LevelSetup LS = combatManager.gameManager.levelManager.level;

        Vector2Int pos = FindRandomSpawn(CombatRoom, LS.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);


        GameObject newAgent = CreateAgent(agentDescription, LS, randomPos);

    }

    public Vector2Int FindRandomSpawn(Room CombatRoom, GridSystem grid)
    {
        int x = Random.Range(CombatRoom.roomBounds.xMin, CombatRoom.roomBounds.xMax - 1);
        int y = Random.Range(CombatRoom.roomBounds.yMin, CombatRoom.roomBounds.yMax - 1);

        while(grid.gridArray[x, y].walkable == false)
        {
            x = Random.Range(CombatRoom.roomBounds.xMin, CombatRoom.roomBounds.xMax - 1);
            y = Random.Range(CombatRoom.roomBounds.yMin, CombatRoom.roomBounds.yMax - 1);
        }

        return new Vector2Int(x,y);
    }
}
