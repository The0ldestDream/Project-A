using UnityEngine;

public class BPTreeTester : MonoBehaviour
{

    public BinaryTree tree = new BinaryTree();
    public GridVisualizer ourGridV;
    public GridSystem ourGrid;
    public int treeDepth = 2;

    // For when I create the actual Tree for the game 
    // I want to feed the dimensions of the grid as the BSP's starting bounds
    // When I want to make bigger floors, I will increase the size of the grid
    // Which in turn will create a bigger space for the BSP to work with



    void Start()
    {
        ourGrid = ourGridV.testGrid;

        tree.maxDepth = treeDepth;
        tree.GenerateTree(new Rect(0, 0, ourGrid.width, ourGrid.height));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
