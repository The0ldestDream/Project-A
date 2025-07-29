using UnityEngine;
using System.Collections.Generic;

public class BTreeVisualizer : MonoBehaviour
{

    public BPTreeTester treetester;
    private BinaryTree vTree;
    public bool showAllNodes = true;
    public bool showLeaf = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vTree = treetester.tree;

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

    }

  
}
