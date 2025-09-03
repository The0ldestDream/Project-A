using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelRenderer : MonoBehaviour
{
    public LevelGenerator lGen;
    public Tilemap tilemap;
    public TileSetData tileData; 

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
    }

    // Update is called once per frame
    void Update()
    {
        if (lGen.generationDone == true)
        {
            RenderLevel();
            lGen.generationDone = false;
        }
    }
}
