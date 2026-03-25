using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GridSystem ourGrid = new GridSystem();
    public BinaryTree tree = new BinaryTree();
    public TerrainGeneration tGen = new TerrainGeneration();
    public DefineRooms defineRooms = new DefineRooms();

    public List<BNode> leafNodes = new List<BNode>();

    public Room spawnRoom;
    public Room stairRoom;

    public void GenerateLevel(LevelData levelData)
    {
        //Generate the Grid
        ourGrid.GenerateGrid(levelData.GridWidth, levelData.GridHeight);

        //Generate BSP
        tree.maxDepth = levelData.maxTreeDepth;
        tree.GenerateTree(new RectInt(0, 0, levelData.GridWidth, levelData.GridHeight));
        leafNodes = tree.getLeafList();

        //Generate Terrain
        tGen.GenerateTerrain(tree, ourGrid);

        //Choose Spawn and Stair Points
        spawnRoom = defineRooms.ChooseSpawnRoom(tGen);
        stairRoom = defineRooms.ChooseStairRoom(tGen);

    }

    public void ClearLevel()
    {
        ourGrid = new GridSystem();
        leafNodes = null;
        spawnRoom = null;
        stairRoom = null;
        tGen.ClearLevel();
    }


}
