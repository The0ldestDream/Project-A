using UnityEngine;

public class EventManager : MonoBehaviour
{

    public LevelGenerator levelGenerator;
    public LevelRenderer levelRenderer;

    public RoomManager roomManager;

    private bool levelGenerated = false;

    public GameObject PlayerAgent;
    public GameObject camera; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomManager.init(levelGenerator);
    }

    // Update is called once per frame
    void Update()
    {
        GenerateEntireLevel(levelGenerated);
        
    }



    private void GenerateEntireLevel(bool levelGen)
    {
        if (levelGen == false)
        {
            levelGenerator.GenerateLevel();
            levelRenderer.RenderLevel();
            levelGenerated = true;
            SpawnPlayer(levelGenerator.spawnRoom);
        }
    }

    private void SpawnPlayer(Room SpawnRoom)
    {
        int x = Random.Range(SpawnRoom.roomBounds.xMin, SpawnRoom.roomBounds.xMax - 1);
        int y = Random.Range(SpawnRoom.roomBounds.yMin, SpawnRoom.roomBounds.yMax - 1);
        Vector3 randomPos = new Vector3(x, y, 0);


        //Workflow for spawning an agent
        GameObject player = Instantiate(PlayerAgent, randomPos, Quaternion.identity);
        PlayerController PC = player.GetComponent<PlayerController>();
        AgentController AC = player.GetComponent<AgentController>();
        Agent playerAgent = new Agent(levelGenerator.ourGrid, levelGenerator.ourGrid.gridArray[x, y], new Human("Human"), new Fighter("Fighter", 1, 1));         
        AC.Init(playerAgent);
        PC.Init(playerAgent, levelGenerator.ourGrid, AC);

        FollowPlayer followPlayer = camera.GetComponent<FollowPlayer>();
        followPlayer.Init(player.transform);
    }
}
