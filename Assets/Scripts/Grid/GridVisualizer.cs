using UnityEngine;

public class GridVisualizer : MonoBehaviour
{

    public GridSystem testGrid = new GridSystem();
    public bool showGrid = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        testGrid.GenerateGrid(200, 200);
        testGrid.setNeighbours();
        testGrid.setWorldPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (showGrid)
        {
            if (testGrid != null && testGrid.gridArray != null)
            {
                Gizmos.color = Color.red;

                // Loop through the gridArray and draw a wire cube at each position
                for (int x = 0; x < testGrid.width; x++)
                {
                    for (int y = 0; y < testGrid.height; y++)
                    {

                        Vector2 size = new Vector2(testGrid.gridArray[x, y].cellwidth, testGrid.gridArray[x, y].cellheight); // Size of each grid cell 
                        Gizmos.DrawWireCube(testGrid.gridArray[x, y].worldPos, size);

                    }
                }

            }
        }
    }
}
