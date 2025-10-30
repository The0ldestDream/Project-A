using UnityEngine;

public class Door
{
    public GridCell doorLocation;

    public bool doorOpen;
    public bool doorLocked;

    public void OpenDoor()
    {
        doorOpen = true;
        doorLocation.walkable = true;
    }

    public void CloseDoor()
    {
        doorOpen = false;
        doorLocation.walkable = false;
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
