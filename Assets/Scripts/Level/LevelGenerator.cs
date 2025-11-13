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

    public int gridSize = 200;
    public int treeDepth = 2;

    public Room spawnRoom;
    public Room stairRoom;

    public void GenerateLevel(int gridSize, int treeDepth)
    {
        //Generate the Grid
        ourGrid.GenerateGrid(gridSize, gridSize);

        //Generate BSP
        tree.maxDepth = treeDepth;
        tree.GenerateTree(new RectInt(0, 0, ourGrid.width, ourGrid.height));
        leafNodes = tree.getLeafList();

        //Generate Terrain
        tGen.GenerateTerrain(tree, ourGrid);

        //Choose Spawn and Stair Points
        spawnRoom = defineRooms.ChooseSpawnRoom(tGen);
        stairRoom = defineRooms.ChooseStairRoom(tGen);

    }


}
