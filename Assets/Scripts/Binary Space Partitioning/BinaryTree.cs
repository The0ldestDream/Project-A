using UnityEngine;
using System.Collections.Generic;

public class BinaryTree
{

    public int maxDepth;
    public int minWidth;
    public int minHeight;

    public int minSize = 10;
    public BNode root;

    public void GenerateTree(RectInt starting_area) // Define a size for the root node
    {
        root = new BNode(starting_area);

        Split(root, maxDepth);
    }

    public List<BNode> getLeafList()
    {
        List<BNode> leaves = new List<BNode>();

        findLeafNodes(root, leaves);

        return leaves;
    }


    private void findLeafNodes(BNode node, List<BNode> leaves)
    {
        //Check is node is valid node
        if (node == null)
        {
            return;
        }
        //Check if node is a leaf. If yes, add node to leaves.
        if (node.isLeaf() == true)
        {
            leaves.Add(node);
        }
        //If no, check its children.
        else
        {
            //Use recursion to go through the children nodes
            findLeafNodes(node.left_child, leaves);
            findLeafNodes(node.right_child, leaves);
        }
    }

    private void Split(BNode node, int maxdepth)
    {
        if (node == null)
        {
            return;
        }



        if (node.depth != maxdepth)
        {
            //Debug.Log("Splitting at depth: " + node.depth);

            int rndInt = Random.Range(0, 2);
            RectInt left_sector = node.sector_bounds;
            RectInt right_sector = node.sector_bounds;

            if (rndInt == 0)
            {
                // Split horizontally - Left Sector is Bottom Half and Right Sector is Top Half

                //Define a min and max values to choose a valid split from
                //Choose a split line
                //Create new values
                int min = node.sector_bounds.y + minSize;
                int max = node.sector_bounds.y + node.sector_bounds.height - minSize;

                if(max<=min)
                {
                    return;
                }
                int splitLine = Random.Range(min,max);



                left_sector.height = splitLine - left_sector.y;
                right_sector.y = splitLine;
                right_sector.height = (node.sector_bounds.y + node.sector_bounds.height) - splitLine;

            }
            else
            {
                // Split Vertically
                int min = node.sector_bounds.x + minSize;
                int max = node.sector_bounds.x + node.sector_bounds.width - minSize;


                if (max <= min)
                {
                    return;
                }

                int splitLine = Random.Range(min, max);

                left_sector.width = splitLine - left_sector.x;
                right_sector.x = splitLine;
                right_sector.width = (node.sector_bounds.x + node.sector_bounds.width) - splitLine;

            }

            BNode leftnode = new BNode(left_sector); // Splitting LOGIC
            node.left_child = leftnode;
            leftnode.parent_node = node;
            leftnode.depth = node.depth + 1;

            Split(leftnode, maxDepth);

            BNode rightnode = new BNode(right_sector); // Splitting LOGIC
            node.right_child = rightnode;
            rightnode.parent_node = node;
            rightnode.depth = node.depth + 1;

            Split(rightnode, maxDepth);
        }
        else
        {
            Debug.Log("Leaf Generated!");
        }

    }

}
