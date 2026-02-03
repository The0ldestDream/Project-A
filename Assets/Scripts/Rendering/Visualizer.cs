using UnityEngine;
using System.Collections.Generic;

public class Visualizer : MonoBehaviour
{
    public LevelGenerator setupTest;

    private GridSystem vGrid;
    private BinaryTree vtree;
    private TerrainGeneration vGen;

    private List<BNode> leafNodes;

    //Toggles
    public bool showGrid = true;
    public bool showAllNodes = false;
    public bool showLeaf = true;
    public bool showRooms = true;
    
    public bool showTiles = false;
    public bool showCorridors = true;
    public bool showRoomTiles = true;
    public bool showWallTiles = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vGrid = setupTest.ourGrid;
        vtree = setupTest.tree;
        vGen = setupTest.tGen;
        leafNodes = setupTest.leafNodes;


    }

    public void drawGrid()
    {
        Gizmos.color = Color.red;
        if (showGrid)
        {
            if (vGrid != null && vGrid.gridArray != null)
            {

                // Loop through the gridArray and draw a wire cube at each position
                for (int x = 0; x < vGrid.width; x++)
                {
                    for (int y = 0; y < vGrid.height; y++)
                    {
                        if (showTiles)
                        {
                            switch (vGrid.gridArray[x, y].TypeOfTile)
                            {
                                case TileType.roomTile:
                                    if (showRoomTiles)
                                    {
                                        Gizmos.color = Color.purple;
                                    }
                                    break;
                                case TileType.corridorTile:
                                    if (showCorridors)
                                    {
                                        Gizmos.color = Color.teal;
                                    }
                                    break;
                                case TileType.doorTile:
                                    Gizmos.color = Color.yellow;
                                    break;
                                case TileType.wallTile:
                                    if (showWallTiles)
                                    {
                                        Gizmos.color = Color.white;
                                    }                                
                                    break;
                                default:
                                    Gizmos.color = Color.clear;
                                    break;
                            }
                        }
                        else
                        {
                            Gizmos.color = Color.clear;
                        }
                        Vector2 size = new Vector2(vGrid.gridArray[x, y].cellwidth, vGrid.gridArray[x, y].cellheight); // Size of each grid cell 
                        Gizmos.DrawWireCube(vGrid.gridArray[x, y].worldPos, size);

                    }
                }

            }
        }
    }

    public void drawNodes(BNode node)
    {
        if (node == null)
        {
            return;
        }
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(node.getCenter(), node.getSectorSize());

        if (node.left_child != null)
        {
            drawNodes(node.left_child);
        }

        if (node.right_child != null)
        {
            drawNodes(node.right_child);
        }
    }
    public void drawLeafNodes(List<BNode> leaves)
    {
        Gizmos.color = Color.blue;
        foreach (BNode leafnode in leaves)
        {
            Gizmos.DrawWireCube(leafnode.getCenter(), leafnode.getSectorSize());
        }
    }

    public void drawRooms(List<Room> rooms)
    {
        Gizmos.color = Color.purple;
        foreach (Room room in rooms)
        {
            Gizmos.DrawWireCube(room.getCenter(), room.getSectorSize());
        }
    }
    public void drawCorridors(List<Corridor> corridors)
    {
        Gizmos.color = Color.yellow;
        foreach (Corridor corridor in corridors)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(corridor.getCenter(corridor.hSeg), corridor.getSectorSize(corridor.hSeg));
            Gizmos.color = Color.teal;
            Gizmos.DrawWireCube(corridor.getCenter(corridor.vSeg), corridor.getSectorSize(corridor.vSeg));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {

        if (vGrid == null)
        {
            return;
        }
        if (showGrid == true)
        {
            drawGrid();
        }
        if (showTiles)
        {
            //drawTiles(vGrid);
        }

        if (vtree == null)
        {
            return;
        }

        if (showAllNodes)
        {

            drawNodes(vtree.root);

        }
        if (showLeaf)
        {
            drawLeafNodes(leafNodes);
        }


        if (vGen == null)
        {
            return;
        }
        if(showRooms)
        {
            drawRooms(vGen.allRooms);
        }

        
    }


}

