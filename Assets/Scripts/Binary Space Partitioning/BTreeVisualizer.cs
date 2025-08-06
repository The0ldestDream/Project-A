using UnityEngine;
using System.Collections.Generic;

public class BTreeVisualizer : MonoBehaviour
{

    public BPTreeTester treetester;
    private BinaryTree vTree;
    private TerrainGeneration vGen;
    public bool showAllNodes = true;
    public bool showLeaf = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vTree = treetester.tree;
        vGen = treetester.tGen;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void OnDrawGizmos()
    {
        

        if (vTree == null)
        {
            return;
        }

        if (showAllNodes)
        {
            
            drawNodes(vTree.root);
         
        }
        if (showLeaf)
        {
            drawLeafNodes(vTree.getLeafList());
        }


        if (vGen == null)
        {
            return;
        }
        drawRooms(vGen.allRooms);
        drawCorridors(vGen.allCorridors);
    }

  
}
