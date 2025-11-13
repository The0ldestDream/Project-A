using UnityEngine;
using System.Collections.Generic;

public class Room
{
    public RectInt roomBounds;
    public int buffer = 3;

    public RoomType TypeOfRoom = RoomType.NormalRoom;

    public List<Door> roomDoors = new List<Door>();

    public RoomState roomState = RoomState.Uncleared;



    public void AddDoor(Door door)
    {

        roomDoors.Add(door);

    }

    public void SetDoorsOwner()
    {
        foreach (Door door in roomDoors)
        {
            door.DoorOwner = this;
        }
    }

    public List<GridCell> GetClosestCellsFrom(Door door, int GivenRange, GridSystem grid)
    {
        List<GridCell> cells = new List<GridCell>();

        for (int x = roomBounds.xMin; x < roomBounds.xMax; x++)
        {
            for (int y = roomBounds.yMin; y < roomBounds.yMax; y++)
            {
                GridCell currentCell = grid.gridArray[x, y];
                
                if (!currentCell.walkable)
                {
                    continue;
                }

                int distanceX = Mathf.Abs(door.doorLocation.x - currentCell.x);
                int distanceY = Mathf.Abs(door.doorLocation.y - currentCell.y);

                if (distanceX <= GivenRange && distanceY <= GivenRange)
                {
                    cells.Add(currentCell);
                }

            }
        }


        return cells;
    }


    public Vector2 getCenter()
    {
        float centerX = (roomBounds.x + (roomBounds.width / 2f));
        float centerY = (roomBounds.y + (roomBounds.height / 2f));
        return new Vector2(centerX, centerY);
    }

    public Vector2 getSectorSize()
    {
        return roomBounds.size;
    }
}
