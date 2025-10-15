using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public List<Room> levelRooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void init(LevelGenerator lgen)
    {
        levelGenerator = lgen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
