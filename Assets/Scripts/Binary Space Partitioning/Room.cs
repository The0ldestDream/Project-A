using UnityEngine;
using System.Collections.Generic;

public class Room
{
    public RectInt roomBounds;
    public int buffer = 3;

    public RoomType TypeOfRoom = RoomType.NormalRoom;

    public List<Door> roomDoors = new List<Door>();


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
