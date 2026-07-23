using UnityEngine;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    public LevelSetup level;
    public Spawner agentSpawner;
    public ItemSpawner itemSpawner;
    public ItemDistributor itemDistributor = new ItemDistributor();

    private int currentFloor = 1;

    public List<AgentController> activeAgents = new List<AgentController>();
    public List<Agent> SavedAgents = new List<Agent>();

    public GameObject camera;
    public GameObject spawnedPlayer;

    public void Init()
    {
        itemDistributor.itemSpawner = itemSpawner;
    }

    public void InitLevel()
    {
        currentFloor = 1;
        level.GenerateEntireLevel(GenerateLevelData(currentFloor));
        SpawnPlayer(level.levelGenerator.spawnRoom);
        // here
        itemDistributor.DistributeItems(level.levelGenerator.tGen);
    }


    private LevelData GenerateLevelData(int FloorNumber)
    {
        LevelData newLevelData = new LevelData();

        newLevelData.LevelNumber = FloorNumber;

        newLevelData.GridHeight = 50 * currentFloor;
        newLevelData.GridWidth = 50 * currentFloor;

        newLevelData.maxTreeDepth = 2 + FloorNumber;

        if (FloorNumber % 2 == 0) // Condition for determining boss floor
        {
            newLevelData.BossFloor = true;
        }
        else
        {
            newLevelData.BossFloor = false;
        }


        return newLevelData;
    }

    public void GenerateNextLevel()
    {
        currentFloor++;

        ClearCurrentLevel();

        LevelData newLevelData = GenerateLevelData(currentFloor);

        level.GenerateEntireLevel(newLevelData);

        SpawnAgents(level.levelGenerator.spawnRoom);

    }

    private void ClearCurrentLevel()
    {
        SaveAgentData();

        level.levelRenderer.tilemap.ClearAllTiles();

        ClearAgents(); //Will need this to change to destroy every game object in scene

        level.levelGenerator.ClearLevel();

    }

    private void SpawnAgents(Room spawnRoom)
    {
        spawnedPlayer = agentSpawner.SpawnPlayer(spawnRoom, SavedAgents[0]);
        FollowPlayer followPlayer = camera.GetComponent<FollowPlayer>();
        followPlayer.Init(spawnedPlayer.transform);
    }

    private void SaveAgentData()
    {
        SavedAgents.Clear();

        foreach (AgentController agent in activeAgents)
        {
            if (agent.myAgent != null && agent.myAgent.IsAlive())
            {
                if (agent.myAgent.alignment == AgentAlignment.Friendly) // Is in Party?
                {
                    SavedAgents.Add(agent.myAgent);
                }
            }
        }

        
    }

    private void ClearAgents()
    {
        foreach (AgentController agent in activeAgents)
        {
            Destroy(agent.gameObject);
        }

        activeAgents.Clear();
    }


    private void SpawnPlayer(Room SpawnRoom)
    {

        AgentDescription agentDescription = new AgentDescription(RaceType.Human, ClassType.Fighter, AgentAlignment.Friendly);

        spawnedPlayer = agentSpawner.SpawnPlayer(SpawnRoom, agentDescription);

        FollowPlayer followPlayer = camera.GetComponent<FollowPlayer>();
        followPlayer.Init(spawnedPlayer.transform);
    }


    public void OnAgentSpawn(Agent agent) 
    {
        activeAgents.Add(agent.controller);

        // Use to subscribe agents to world events
        agent.OnDeath += OnAgentDeath;



        agent.OnSpawn -= OnAgentSpawn; // Just needs to be declared once i think
    }

    public void OnAgentDeath(Agent agent)
    {
        //Clear GridCell etc

        level.levelGenerator.ourGrid.gridArray[agent.gridPos.x, agent.gridPos.y].EntityOnTile = EntityType.None;
        level.levelGenerator.ourGrid.gridArray[agent.gridPos.x, agent.gridPos.y].AgentOnTile = null;
        level.levelGenerator.ourGrid.gridArray[agent.gridPos.x, agent.gridPos.y].damageable = null;
        level.levelGenerator.ourGrid.gridArray[agent.gridPos.x, agent.gridPos.y].walkable = true;


        activeAgents.Remove(agent.controller);

        //Unsub agents from world events
        agent.OnDeath -= OnAgentDeath;
    }

    private void OnEnable()
    {
        Stairs.OnStairsReached += GenerateNextLevel;
    }
    private void OnDisable()
    {
        Stairs.OnStairsReached -= GenerateNextLevel;

    }
}
