using UnityEngine;
using System.Collections.Generic;


public class TerrainGeneration
{
    public List<BNode> leafNodes;

    public List<Room> allRooms = new List<Room>();
    public List<Corridor> allCorridors = new List<Corridor>();
    private AStarPathfinding AStarPathfinder = new AStarPathfinding();
    private GridSystem gridSystem;

    public void GenerateTerrain(BinaryTree tree, GridSystem GS)
    {
        gridSystem = GS;
        leafNodes = tree.getLeafList();

        GenerateRooms(); // Generate the Rooms
        SetTempTileTypes(gridSystem); // Set Tiles around the rooms
        ConnectNodes(tree.root); // Generate the Corridors
        SetFinalTileTypes(gridSystem); // Set Tiles around the Corridors


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

        //Vector2Int pointA = new Vector2Int(Random.Range(leftRoom.roomBounds.xMin,leftRoom.roomBounds.xMax-1), Random.Range(leftRoom.roomBounds.yMin, leftRoom.roomBounds.yMax - 1));
        //Vector2Int pointB = new Vector2Int(Random.Range(rightRoom.roomBounds.xMin, rightRoom.roomBounds.xMax - 1), Random.Range(rightRoom.roomBounds.yMin, rightRoom.roomBounds.yMax - 1));
        HashSet<Vector2Int> pointsA = GetEdgePoints(leftRoom, true);
        HashSet<Vector2Int> pointsB = GetEdgePoints(rightRoom, true);
        var Points = GetClosestPoints(pointsA, pointsB);
        
        Vector2Int pointA = Points.pointA;
        Vector2Int pointB = Points.pointB;

        List<GridCell> CorridorCells = AStarPathfinder.Pathfinding(gridSystem.gridArray[pointA.x, pointA.y], gridSystem.gridArray[pointB.x,pointB.y], gridSystem);

        Corridor newCorridor = new Corridor();

        foreach (GridCell cell in CorridorCells)
        {
            newCorridor.cTiles.Add(new Vector2Int(cell.x, cell.y));
        }


        allCorridors.Add(newCorridor);
    }

    private RectInt GenerateRandomRoomSize(RectInt leafarea)
    {

        //Decide on a random width and height within the size of the section
        //Placeholder Calculation
        //For the min value, I just decided the room should be at least of the size of the subsection
        //Definatley need some checks here to make sure the room has a valid size
        int roomWidth = Random.Range((leafarea.width / 2), leafarea.width-2);
        int roomHeight = Random.Range((leafarea.height / 2), leafarea.height - 2);

        //Decide on a position in the section to have the room spawn in
        //For more variance

        int x = Random.Range(leafarea.x + 1, leafarea.x + (leafarea.width - roomWidth));
        int y = Random.Range(leafarea.y + 1, leafarea.y + (leafarea.height - roomHeight));


        return new RectInt(x,y, roomWidth,roomHeight);
    }

    private HashSet<Vector2Int> GetEdgePoints(Room room, bool RemoveCornerPoints)
    {
        
        HashSet<Vector2Int> edgePoints = new HashSet<Vector2Int>();

        //Four Edges
        //Bottom Left to Bottom Right = (xmin,ymin) -> (xmax,ymin)
        //Bottom Left to Top Left = (xmin,ymin) -> (xmin,ymax)
        //Bottom Right to Top Right = (xmax,ymin) -> (xmax,ymax)
        //Top Left to Top Right = (xmin,ymax) -> (xmax,ymax)

        Vector2Int botL = new Vector2Int(room.roomBounds.x, room.roomBounds.y);
        Vector2Int botR = new Vector2Int(room.roomBounds.xMax-1, room.roomBounds.y);
        Vector2Int topL = new Vector2Int(room.roomBounds.x, room.roomBounds.yMax-1);
        Vector2Int topR = new Vector2Int(room.roomBounds.xMax-1, room.roomBounds.yMax-1);

        for (int x = botL.x; x <= botR.x; x++)
        {
            edgePoints.Add(new Vector2Int(x, botR.y));
        }
        for (int y = botL.y; y <= topL.y; y++)
        {
            edgePoints.Add(new Vector2Int(topL.x, y));
        }
        for (int y = botR.y; y <= topR.y; y++)
        {
            edgePoints.Add(new Vector2Int(topR.x, y));
        }
        for (int x = topL.x; x <= topR.x; x++)
        {
            edgePoints.Add(new Vector2Int(x, topR.y));
        }

        if (RemoveCornerPoints == true)
        {

            edgePoints.Remove(botL);
            edgePoints.Remove(botR);
            edgePoints.Remove(topR);
            edgePoints.Remove(topL);


        }
        
        
        return edgePoints;
    }

    private (Vector2Int pointA, Vector2Int pointB) GetClosestPoints(HashSet<Vector2Int> edgesA, HashSet<Vector2Int> edgesB)
    {
        Vector2Int pointA = new Vector2Int();
        Vector2Int pointB = new Vector2Int();


        Vector2Int currentShortestPointA = new Vector2Int();
        Vector2Int currentShortestPointB = new Vector2Int();
        int shortestManhattanDistance = int.MaxValue; 

        foreach (Vector2Int edgeA in edgesA)
        {
            foreach (Vector2Int edgeB in edgesB)
            {

                int ManhattanDistance;

                ManhattanDistance = Mathf.Abs(edgeA.x - edgeB.x) + Mathf.Abs(edgeA.y - edgeB.y);

                if (ManhattanDistance < shortestManhattanDistance)
                {
                    shortestManhattanDistance = ManhattanDistance;
                    currentShortestPointA = edgeA;
                    currentShortestPointB = edgeB;
                }

            }
        }

        pointA = currentShortestPointA;
        pointB = currentShortestPointB;

        return (pointA, pointB);
    }



    private void SetTempTileTypes(GridSystem gridSystem)
    {
        //Set Corridors tiles first then Room tiles
        //This way Room tiles can overwrite the Corridors if the are reaching/passing through a room
        GridCell[,] gridCells = gridSystem.gridArray;



        foreach (Room room in allRooms)
        {
            int minX = room.roomBounds.xMin;
            int maxX = room.roomBounds.xMax-1;
            int minY = room.roomBounds.yMin;
            int maxY = room.roomBounds.yMax-1;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    gridCells[x, y].TypeOfTile = TileType.roomTile;
                    gridCells[x, y].walkable = false;
                    //Debug.Log("Room Tile Set");
                 
                }
            }

            HashSet<Vector2Int> roomEdges = GetEdgePoints(room, false);
            foreach (Vector2Int point in roomEdges)
            {

                gridCells[point.x, point.y].TypeOfTile = TileType.wallTile;
                gridCells[point.x, point.y].walkable = true;
            }
            foreach (Vector2Int point in roomEdges)
            {
                for (int x = 0; x < gridCells[point.x,point.y].neighbours.Count; x++)
                {
                    if (gridCells[point.x, point.y].neighbours[x].TypeOfTile == TileType.emptyTile)
                    {
                        gridCells[point.x, point.y].neighbours[x].cost = 5;
                    }
                }
            }

        }

    }

    private void SetFinalTileTypes(GridSystem gridSystem)
    {
        //Find all grid cells that are corridor tiles
        //Then check its neighbours for grid cells that are room tiles
        //Then change those into door tiles
        //Maybe check if its next to another corridor then extend that door tile?

        //Since the room tiles override the corridor tiles, we need to remove the room tile cells from the list

        GridCell[,] gridCells = gridSystem.gridArray;


        List<GridCell> corridorCells = new List<GridCell>();

        for (int X = 0; X < allCorridors.Count; X++)
        {
            for (int Y = 0; Y < allCorridors[X].cTiles.Count; Y++)
            {
                gridCells[allCorridors[X].cTiles[Y].x, allCorridors[X].cTiles[Y].y].TypeOfTile = TileType.corridorTile;
                corridorCells.Add(gridCells[allCorridors[X].cTiles[Y].x, allCorridors[X].cTiles[Y].y]);
            }
        }

        corridorCells.RemoveAll(x => x.TypeOfTile == TileType.roomTile);

        foreach (Room room in allRooms)
        {
            int minX = room.roomBounds.xMin;
            int maxX = room.roomBounds.xMax - 1;
            int minY = room.roomBounds.yMin;
            int maxY = room.roomBounds.yMax - 1;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    gridCells[x, y].TypeOfTile = TileType.roomTile;
                    gridCells[x, y].walkable = true;

                }
            }

            HashSet<Vector2Int> roomEdges = GetEdgePoints(room, false);
            foreach (Vector2Int point in roomEdges)
            {

                gridCells[point.x, point.y].TypeOfTile = TileType.wallTile;
                gridCells[point.x, point.y].walkable = false;
            }
            foreach (Vector2Int point in roomEdges)
            {
                for (int x = 0; x < gridCells[point.x, point.y].neighbours.Count; x++)
                {
                    if (gridCells[point.x, point.y].neighbours[x].TypeOfTile == TileType.emptyTile)
                    {
                        gridCells[point.x, point.y].neighbours[x].cost = 0;
                        gridCells[point.x, point.y].neighbours[x].walkable = false;
                    }

                    if (gridCells[point.x, point.y].neighbours[x].TypeOfTile == TileType.corridorTile)
                    {
                        gridCells[point.x, point.y].TypeOfTile = TileType.doorTile;
                    }

                }
            }

        }






        //Walls around Corridors
        for (int x = 0; x < corridorCells.Count; x++)
        {
            for (int i = 0; i < corridorCells[x].neighbours.Count; i++)
            {
                if (corridorCells[x].neighbours[i].TypeOfTile == TileType.emptyTile)
                {
                    corridorCells[x].neighbours[i].TypeOfTile = TileType.wallTile;
                    corridorCells[x].neighbours[i].walkable = false;
                }
            }
        }

    }



}
