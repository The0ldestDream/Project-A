using UnityEngine;
using System;

public class Door
{
    //Class Information
    public GridCell doorLocation;
    public bool doorOpen;
    public bool doorLocked = false;

    public Room DoorOwner;

    public Door()
    {
        GameManager.OnPlayerInteracted += InteractedDoor;
    }

    //Events


    public void DeleteDoor()
    {
        GameManager.OnPlayerInteracted -= InteractedDoor;
    }

    public void InteractedDoor(Door door)
    {
        if (door == this)
        {
            if (doorOpen == false && doorLocked != true)
            {
                doorOpen = true;
                doorLocation.walkable = true;
                Debug.Log("Door Opened");
            }
            else
            {
                doorOpen = false;
                doorLocation.walkable = false;
                Debug.Log("Door Closed");
            }
        }


    }

    public void LockDoor()
    {
        doorLocked = true;
    }

    public void UnlockDoor()
    {
        doorLocked = false;
    }


}
