using UnityEngine;
using System.Collections.Generic;

public class SetupTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GridSystem testGrid = new GridSystem();
    public BinaryTree tree = new BinaryTree();
    public TerrainGeneration tGen = new TerrainGeneration();


    public List<BNode> leafNodes = new List<BNode>();


    public int gridSize = 200;
    public int treeDepth = 2;

    void Awake()
    {
        //Generate the Grid
        testGrid.GenerateGrid(gridSize, gridSize);
        testGrid.setNeighbours();
        testGrid.setWorldPositions();

        //Generate BSP
        tree.maxDepth = treeDepth;
        tree.GenerateTree(new RectInt(0, 0, testGrid.width, testGrid.height));
        leafNodes = tree.getLeafList();

        //Generate Terrain
        tGen.GenerateTerrain(tree);

        //Tell the grid which cells have become Rooms and Corridors
        tGen.SetTileTypes(testGrid);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
