using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    public CombatManager combatManager;

    public GameObject playerPrefab;
    public GameObject agentPrefab;


    public List<GameObject> EnemyAgents = new List<GameObject>();
    public List<GameObject> FriendlyAgents = new List<GameObject>();


    private GameObject CreateAgent(AgentDescription description, LevelManager LM, Vector3 spawnPos) // For non player agent
    {
        GridSystem grid = LM.level.levelGenerator.ourGrid;

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


        newAgent.OnSpawn += LM.OnAgentSpawn;

        newAgent.AgentSpawn();

        return agent;
    }

    private GameObject CreateAgent(Agent agent, LevelManager LM, Vector3 spawnPos)
    {
        GridSystem grid = LM.level.levelGenerator.ourGrid;

        agent.gridPos = grid.gridArray[(int)spawnPos.x, (int)spawnPos.y];

        //Workflow for spawning an agent
        GameObject newagent = Instantiate(agentPrefab, spawnPos, Quaternion.identity);
        AgentController AC = newagent.GetComponent<AgentController>();
        AC.Init(agent);
        AC.pathfinding = combatManager.gameManager.pathfinding;
        AIController AIC = new AIController(agent);
        AC.AIC = AIC;

        if (agent.alignment == AgentAlignment.Friendly)
        {
            FriendlyAgents.Add(newagent);
        }
        else if (agent.alignment == AgentAlignment.Enemy)
        {
            EnemyAgents.Add(newagent);
        }
        else
        {
            //For neutral agents, I don't know if i will have any for a while
        }



        return newagent;
    }
    public void SpawnAgent(Room SpawnRoom, AgentDescription agentDescription)
    {
        LevelManager LM = combatManager.gameManager.levelManager;

        Vector2Int pos = FindRandomSpawn(SpawnRoom, LM.level.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);


        GameObject newAgent = CreateAgent(agentDescription, LM, randomPos);

    }
    public void SpawnAgent(Room SpawnRoom, Agent agent)
    {
        LevelManager LM = combatManager.gameManager.levelManager;

        Vector2Int pos = FindRandomSpawn(SpawnRoom, LM.level.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);


        GameObject newAgent = CreateAgent(agent, LM, randomPos);
    }

    private GameObject CreatePlayer(AgentDescription description, LevelManager LM, Vector3 spawnPos)
    {
        GridSystem grid = LM.level.levelGenerator.ourGrid;


        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        PlayerController PC = player.GetComponent<PlayerController>();
        AgentController AC = player.GetComponent<AgentController>();

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

        AC.Init(newAgent);
        PC.Init(newAgent, grid, AC);

        newAgent.OnSpawn += LM.OnAgentSpawn;

        newAgent.AgentSpawn();

        return player;
    }

    private GameObject CreatePlayer(Agent agent, LevelManager LM, Vector3 spawnPos)
    {
        GridSystem grid = LM.level.levelGenerator.ourGrid;

        agent.gridPos = grid.gridArray[(int)spawnPos.x, (int)spawnPos.y];

        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        PlayerController PC = player.GetComponent<PlayerController>();
        AgentController AC = player.GetComponent<AgentController>();


        AC.Init(agent);
        PC.Init(agent, grid, AC);

        return player;
    }

    public GameObject SpawnPlayer(Room SpawnRoom, AgentDescription description)
    {
        LevelManager LM = combatManager.gameManager.levelManager;

        Vector2Int pos = FindRandomSpawn(SpawnRoom, LM.level.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);


        GameObject newAgent = CreatePlayer(description, LM, randomPos);

        return newAgent;
    }

    public GameObject SpawnPlayer(Room SpawnRoom, Agent agent)
    {
        LevelManager LM = combatManager.gameManager.levelManager;

        Vector2Int pos = FindRandomSpawn(SpawnRoom, LM.level.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);


        GameObject newAgent = CreatePlayer(agent, LM, randomPos);

        return newAgent;
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
