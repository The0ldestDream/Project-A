using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GridSystem ourGrid = new GridSystem();
    public BinaryTree tree = new BinaryTree();
    public TerrainGeneration tGen = new TerrainGeneration();


    public List<BNode> leafNodes = new List<BNode>();

    public bool generationDone = false;

    public int gridSize = 200;
    public int treeDepth = 2;

    void Awake()
    {
        //Generate the Grid
        ourGrid.GenerateGrid(gridSize, gridSize);
        ourGrid.setNeighbours();
        ourGrid.setWorldPositions();

        //Generate BSP
        tree.maxDepth = treeDepth;
        tree.GenerateTree(new RectInt(0, 0, ourGrid.width, ourGrid.height));
        leafNodes = tree.getLeafList();

        //Generate Terrain
        tGen.GenerateTerrain(tree, ourGrid);


        generationDone = true;
        //Render Level

    }

    // Update is called once per frame
    void Update()
    {

    }
}
