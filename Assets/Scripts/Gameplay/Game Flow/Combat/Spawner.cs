using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    public CombatManager combatManager;
    public GameObject EnemyAgent;


    public List<GameObject> EnemyAgents = new List<GameObject>();

    private List<AgentRace> agentRaces = new List<AgentRace> {new Human()};
    private List<AgentClass> agentClasses = new List<AgentClass> { new Fighter(1, 1) };

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAgent(Room CombatRoom)
    {
        int x = Random.Range(CombatRoom.roomBounds.xMin, CombatRoom.roomBounds.xMax - 1);
        int y = Random.Range(CombatRoom.roomBounds.yMin, CombatRoom.roomBounds.yMax - 1);
        Vector3 randomPos = new Vector3(x, y, 0);

        LevelSetup LS = combatManager.gameManager.level;

        //Workflow for spawning an agent
        GameObject agent = Instantiate(EnemyAgent, randomPos, Quaternion.identity);
        AgentController AC = agent.GetComponent<AgentController>();
        Agent EAgent = new Agent(LS.levelGenerator.ourGrid, LS.levelGenerator.ourGrid.gridArray[x, y], new Human(), new Fighter(1, 1), AgentAlignment.Enemy);
        AC.Init(EAgent);
        AC.pathfinding = combatManager.gameManager.pathfinding;

        EnemyAgents.Add(agent);

    }
}
