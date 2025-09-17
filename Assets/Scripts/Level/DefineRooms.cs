using UnityEngine;

public class DefineRooms
{
    
    public Room ChooseSpawnRoom(TerrainGeneration tGen)
    {

        int RanRoom = Random.Range(0 ,tGen.allRooms.Count);
        Room randomRoom = new Room();

        if (tGen.allRooms[RanRoom].TypeOfRoom != RoomType.StairRoom && tGen.allRooms[RanRoom].TypeOfRoom != RoomType.BossRoom)
        {
            tGen.allRooms[RanRoom].TypeOfRoom = RoomType.SpawnRoom;
            randomRoom = tGen.allRooms[RanRoom];
        }

        return randomRoom;

    }

    public Room ChooseStairRoom(TerrainGeneration tGen)
    {
        int RanRoom = Random.Range(0, tGen.allRooms.Count);
        Room randomRoom = new Room();

        if (tGen.allRooms[RanRoom].TypeOfRoom != RoomType.SpawnRoom)
        {
            tGen.allRooms[RanRoom].TypeOfRoom = RoomType.StairRoom;
            randomRoom = tGen.allRooms[RanRoom];
        }

        return randomRoom;
    }


}
