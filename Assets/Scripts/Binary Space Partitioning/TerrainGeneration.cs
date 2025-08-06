using UnityEngine;
using System.Collections.Generic;


public class TerrainGeneration
{
    public List<BNode> leafNodes;

    public List<Room> allRooms = new List<Room>();
    public List<Corridor> allCorridors = new List<Corridor>();

    public void GenerateTerrain(BinaryTree tree)
    {
        leafNodes = tree.getLeafList();

        GenerateRooms();
        ConnectNodes(tree.root);

    }

    private void GenerateRooms()
    {
        for (int x = 0; x < leafNodes.Count; x++)
        {
            RectInt leafarea = leafNodes[x].sector_bounds;

            Room newRoom = new Room();

            newRoom.roomBounds = GenerateRandomRoomSize(leafarea);

            allRooms.Add(newRoom);
            leafNodes[x].assignedRoom = newRoom;
        }

    }

    private Room ConnectNodes(BNode node)
    {
        //Post Order Traversal 
        if (node.isLeaf() == true)
        {
            return node.assignedRoom;
        }

        Room leftRoom = ConnectNodes(node.left_child);
        Room rightRoom = ConnectNodes(node.right_child);
        GenerateCorridor(leftRoom, rightRoom);

        //Choose Random one to pass up
        if (Random.Range(0,100) > 50)
        {

            return leftRoom;
        }
        else
        {

            return rightRoom;
        }
    }


    private void GenerateCorridor(Room leftRoom, Room rightRoom)
    {
        //Creating Corridors
        //I want to create L shape corridors
        //Loop in one direction until the pointa x or y matchs the pointb x or y
        //Loop in the other direction until we arrive at pointb


        Vector2Int pointA = new Vector2Int(Random.Range(leftRoom.roomBounds.xMin,leftRoom.roomBounds.xMax-1), Random.Range(leftRoom.roomBounds.yMin, leftRoom.roomBounds.yMax - 1));
        Vector2Int pointB = new Vector2Int(Random.Range(rightRoom.roomBounds.xMin, rightRoom.roomBounds.xMax - 1), Random.Range(rightRoom.roomBounds.yMin, rightRoom.roomBounds.yMax - 1));


        Corridor newCorridor = new Corridor();

        if (Random.Range(0, 100) > 50)
        {
            //Horizontal first
            for (int x= Mathf.Min(pointA.x, pointB.x); x <= Mathf.Max(pointA.x,pointB.x); x++)
            {
                newCorridor.cTiles.Add(new Vector2Int(x, pointA.y));
                Debug.Log("Horizontal loop finished");

            }
            newCorridor.hSeg = new RectInt(Mathf.Min(pointA.x, pointB.x),pointA.y,Mathf.Abs(pointB.x - pointA.x)+1, 1);


            //Vertical Second
            for (int y = Mathf.Min(pointA.y, pointB.y); y <= Mathf.Max(pointA.y, pointB.y); y++)
            {
                newCorridor.cTiles.Add(new Vector2Int(pointB.x, y));
            }
            newCorridor.vSeg = new RectInt(pointB.x, Mathf.Min(pointA.y, pointB.y), 1, Mathf.Abs(pointB.y - pointA.y) + 1);
        }
        else
        {
            //Vertical first
            for (int y = Mathf.Min(pointA.y, pointB.y); y <= Mathf.Max(pointA.y, pointB.y); y++)
            {
                newCorridor.cTiles.Add(new Vector2Int(pointA.x, y));
            }
            newCorridor.vSeg = new RectInt(pointB.x, Mathf.Min(pointA.y, pointB.y), 1, Mathf.Abs(pointB.y - pointA.y) + 1);
            //Horizontal Second
            for (int x = Mathf.Min(pointA.x, pointB.x); x <= Mathf.Max(pointA.x, pointB.x); x++)
            {
                newCorridor.cTiles.Add(new Vector2Int(x, pointB.y));
            }
            newCorridor.hSeg = new RectInt(Mathf.Min(pointA.x, pointB.x), pointA.y, Mathf.Abs(pointB.x - pointA.x) + 1, 1);
        }


        allCorridors.Add(newCorridor);
    }


    private RectInt GenerateRandomRoomSize(RectInt leafarea)
    {

        //Decide on a random width and height within the size of the section
        //Placeholder Calculation
        //For the min value, I just decided the room should be at least of the size of the subsection
        //Definatley need some checks here to make sure the room has a valid size
        int roomWidth = Random.Range((leafarea.width / 2), leafarea.width-1);
        int roomHeight = Random.Range((leafarea.height / 2), leafarea.height - 1);

        //Decide on a position in the section to have the room spawn in
        //For more variance

        int x = Random.Range(leafarea.x + 1, leafarea.x + (leafarea.width - roomWidth));
        int y = Random.Range(leafarea.y + 1, leafarea.y + (leafarea.height - roomHeight));


        return new RectInt(x,y, roomWidth,roomHeight);
    }

}
