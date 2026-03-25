using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelSetup level;
    private int currentFloor = 1;

    public void Init()
    {

    }

    public void InitLevel()
    {
        currentFloor = 1;
        level.GenerateEntireLevel(GenerateLevelData(currentFloor));
    }


    private LevelData GenerateLevelData(int FloorNumber)
    {
        LevelData newLevelData = new LevelData();

        newLevelData.GridHeight = 50 * currentFloor;
        newLevelData.GridWidth = 50 * currentFloor;

        newLevelData.maxTreeDepth = 2 + FloorNumber;

        return newLevelData;
    }

    public void GenerateNextLevel()
    {
        currentFloor++;

        ClearCurrentLevel();

        LevelData newLevelData = GenerateLevelData(currentFloor);

        level.GenerateEntireLevel(newLevelData);

    }

    private void ClearCurrentLevel()
    {
        level.levelRenderer.tilemap.ClearAllTiles();

        GameObject.Destroy(level.spawnedPlayer); //Will need this to change to destroy every game object in scene

        level.levelGenerator.ClearLevel();

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
