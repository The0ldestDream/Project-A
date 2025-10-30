using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    
    // Room Manager
    // Should handle what happens within the room the player is in/entering
    // Hanldes locking and unlocking doors when the enters/clears said room
    // Maybe even handle creating/destroying combat grid 




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void init(LevelGenerator lgen)
    {
        levelGenerator = lgen;
    }


    public void LockAllRoomDoors(Room room)
    {
        foreach (Door door in room.roomDoors)
        {
            door.LockDoor();
        }
    }

    public void UnlockAllRoomDoors(Room room)
    {
        foreach (Door door in room.roomDoors)
        {
            door.UnlockDoor();
        }
    }


}
