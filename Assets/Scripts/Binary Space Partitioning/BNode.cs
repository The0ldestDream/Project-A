using UnityEngine;

public class BNode
{
    public BNode parent_node;
    public BNode left_child;
    public BNode right_child;

    private const int tileSize = 1;
    public RectInt sector_bounds; 
    public int depth = 0;

    public BNode(RectInt bounds)
    {
        parent_node = null;
        left_child = null;
        right_child = null;
        sector_bounds = bounds; //Dimensions of the Node's Section
    }

    public Vector2 getCenter()
    {
        float centerX = (sector_bounds.x + (sector_bounds.width / 2f)) * tileSize;
        float centerY = (sector_bounds.y + (sector_bounds.height / 2f)) * tileSize;
        return new Vector2(centerX, centerY);

    }

    public Vector2 getSectorSize()
    {
        return sector_bounds.size * tileSize;
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
