using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelRenderer : MonoBehaviour
{
    public LevelGenerator lGen;
    public Tilemap tilemap;
    public TileSetData tileData;

    public Tilemap HighlightMap;
    public HighlightTilesetData highlighttileData;

    private bool renderLevel = false;

    public void RenderLevel()
    {
        for (int x =0; x < lGen.ourGrid.width; x++)
        {
            for (int y = 0; y < lGen.ourGrid.height; y++)
            {
                Vector3Int cellPos = new Vector3Int(lGen.ourGrid.gridArray[x, y].x, lGen.ourGrid.gridArray[x, y].y, 0);


                if (lGen.ourGrid.gridArray[x, y].TypeOfTile == TileType.corridorTile || lGen.ourGrid.gridArray[x, y].TypeOfTile == TileType.roomTile)
                {
                    tilemap.SetTile(cellPos, tileData.floor);
                }
                else if (lGen.ourGrid.gridArray[x, y].TypeOfTile == TileType.wallTile)
                {
                    tilemap.SetTile(cellPos, tileData.wall);
                }
                else if (lGen.ourGrid.gridArray[x, y].TypeOfTile == TileType.doorTile)
                {
                    tilemap.SetTile(cellPos, tileData.door);
                }

            }
        }

        foreach (Room room in lGen.tGen.allRooms)
        {
            if (room.TypeOfRoom == RoomType.SpawnRoom)
            {
                int x = Random.Range(room.roomBounds.xMin + 2, room.roomBounds.xMax - 2);
                int y = Random.Range(room.roomBounds.yMin + 2, room.roomBounds.yMax - 2);
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                tilemap.SetTile(cellPos, tileData.spawn);

            }
            if (room.TypeOfRoom == RoomType.StairRoom)
            {
                int x = Random.Range(room.roomBounds.xMin + 2, room.roomBounds.xMax - 2);
                int y = Random.Range(room.roomBounds.yMin + 2, room.roomBounds.yMax - 2);
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                tilemap.SetTile(cellPos, tileData.stairs);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (renderLevel)
        {

        }
    }
}
