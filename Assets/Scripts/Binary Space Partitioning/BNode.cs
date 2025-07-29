using UnityEngine;

public class BNode
{
    public BNode parent_node;
    public BNode left_child;
    public BNode right_child;


    public Rect sector_bounds; 
    public int depth = 0;

    public BNode(Rect bounds)
    {
        parent_node = null;
        left_child = null;
        right_child = null;
        sector_bounds = bounds; //Dimensions of the Node's Section
    }

    public Vector2 getCenter()
    {
        return sector_bounds.center;
    }

    public Vector2 getSectorSize()
    {
        return sector_bounds.size;
    }


    public bool isRoot()
    {
        if (parent_node == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isLeaf()
    {
        if (left_child == null && right_child == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
