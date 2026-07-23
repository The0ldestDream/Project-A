using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public CombatManager combatManager;

    private GameObject CreateItem(ItemDescription itemDescription, LevelManager LM, Vector3 spawnPos)
    {
        GridSystem grid = LM.level.levelGenerator.ourGrid;

        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        ItemController ic = item.GetComponent<ItemController>();

        switch (itemDescription.type)
        {
            case (ItemType.Consumable):
                Item conItem = new ConsumableItem(itemDescription);
                conItem.gridPos = grid.gridArray[(int)spawnPos.x, (int)spawnPos.y];
                conItem.SetTile(conItem.gridPos, true);
                ic.Init(conItem);
                break;
            case (ItemType.Equipment):
                Item equipItem = new EquippableItem(itemDescription);
                equipItem.gridPos = grid.gridArray[(int)spawnPos.x, (int)spawnPos.y];
                equipItem.SetTile(equipItem.gridPos, true);
                ic.Init(equipItem);
                break;
        }

        return item;
    }

    public GameObject SpawnItem(Room SpawnRoom, ItemDescription itemDescription)
    {
        LevelManager LM = combatManager.gameManager.levelManager;

        Vector2Int pos = FindRandomSpawn(SpawnRoom, LM.level.levelGenerator.ourGrid);
        Vector3 randomPos = new Vector3(pos.x, pos.y, 0);

        GameObject newItem = CreateItem(itemDescription, LM, randomPos);

        return newItem;
    }

    public Vector2Int FindRandomSpawn(Room room, GridSystem grid)
    {
        int x = Random.Range(room.roomBounds.xMin, room.roomBounds.xMax - 1);
        int y = Random.Range(room.roomBounds.yMin, room.roomBounds.yMax - 1);

        while (grid.gridArray[x, y].walkable == false)
        {
            x = Random.Range(room.roomBounds.xMin, room.roomBounds.xMax - 1);
            y = Random.Range(room.roomBounds.yMin, room.roomBounds.yMax - 1);
        }

        return new Vector2Int(x, y);
    }
}
