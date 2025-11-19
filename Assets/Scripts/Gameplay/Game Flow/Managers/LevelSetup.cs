using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public LevelRenderer levelRenderer;

    public bool levelGenerated = false;

    public GameObject PlayerAgent;
    public GameObject spawnedPlayer;

    public GameObject camera;


    //Events


    void Init()
    {

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
            levelGenerator.GenerateLevel(200,4);
            levelRenderer.RenderLevel();
            
            SpawnPlayer(levelGenerator.spawnRoom);
            levelGenerated = true;
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
        Agent playerAgent = new Agent(levelGenerator.ourGrid, levelGenerator.ourGrid.gridArray[x, y], new Human(), new Fighter(1, 1), AgentAlignment.Friendly);
        AC.Init(playerAgent);
        PC.Init(playerAgent, levelGenerator.ourGrid, AC);


        spawnedPlayer = player;

        FollowPlayer followPlayer = camera.GetComponent<FollowPlayer>();
        followPlayer.Init(player.transform);
    }
}
