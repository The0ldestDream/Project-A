using UnityEngine;

public class DefineRooms
{
    
    public Room ChooseSpawnRoom(TerrainGeneration tGen)
    {

        int RanRoom = Random.Range(0 ,tGen.allRooms.Count);
        Room randomRoom = new Room();

        if (!tGen.allRooms[RanRoom].StairRoom && tGen.allRooms[RanRoom].TypeOfRoom != RoomType.BossRoom)
        {
            tGen.allRooms[RanRoom].TypeOfRoom = RoomType.SpawnRoom;
            randomRoom = tGen.allRooms[RanRoom];
        }

        randomRoom.ChangeRoomState(RoomState.Cleared);

        return randomRoom;

    }

    public Room ChooseStairRoom(TerrainGeneration tGen)
    {
        int RanRoom = Random.Range(0, tGen.allRooms.Count);
        Room randomRoom = new Room();

        if (tGen.allRooms[RanRoom].TypeOfRoom != RoomType.SpawnRoom)
        {
            tGen.allRooms[RanRoom].StairRoom = true;
            randomRoom = tGen.allRooms[RanRoom];

            tGen.SetStairTile(randomRoom);

        }

        return randomRoom;
    }

    public Room ChooseBossRoom(TerrainGeneration tGen)
    {
        int RanRoom = Random.Range(0, tGen.allRooms.Count);
        Room randomRoom = new Room();

        if (tGen.allRooms[RanRoom].TypeOfRoom != RoomType.SpawnRoom)
        {
            tGen.allRooms[RanRoom].TypeOfRoom = RoomType.BossRoom;
            randomRoom = tGen.allRooms[RanRoom];

        }

        return randomRoom;
    }


    public void AssignRoomTypes(TerrainGeneration tGen)
    {
        //Here I can randomly assign rooms their types
        //Some rooms can be extra difficult by having elite units spawn (elite like in Risk Of Rain)
        //Some rooms can be shop rooms or heal rooms like in PMD and Hades
        //Assign the types here and then handling the generation maybe in the encounter system?
    }
}
