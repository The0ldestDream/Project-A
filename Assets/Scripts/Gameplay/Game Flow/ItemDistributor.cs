using UnityEngine;

public class ItemDistributor
{
    public ItemDatabase database = new ItemDatabase();
    public ItemSpawner itemSpawner;


    public void DistributeItems(TerrainGeneration tGen)
    {
        foreach (Room room in tGen.allRooms)
        {
            
            itemSpawner.SpawnItem(room, database.GetDescription(ItemID.HealthPotion));


        }

    }

}
