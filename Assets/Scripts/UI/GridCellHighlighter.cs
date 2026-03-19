using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class GridCellHighlighter : MonoBehaviour
{
    public LevelGenerator lGen;
    public Tilemap HightlightMap;
    public HighlightTilesetData tileData;

    public GridSystem grid;
    public void InitHighlighter()
    {
        grid = lGen.ourGrid;
    }

    public void HighlightTiles(List<GridCell> cells)
    {
        HightlightMap.ClearAllTiles();
        foreach (GridCell cell in cells)
        {
            HighlightTile(cell);
        }
    }

    public void HighlightTile(GridCell cell)
    {
        
        Vector3Int cellPos = new Vector3Int(cell.x, cell.y, 0);
        HightlightMap.SetTile(cellPos, tileData.TargetTile);
        
    }

    public void ClearTiles(List<GridCell> cells)
    {
        foreach (GridCell cell in cells)
        {
            ClearTile(cell);
        }

    }

    public void ClearTile(GridCell cell)
    {
        Vector3Int cellPos = new Vector3Int(cell.x, cell.y, 0);

        HightlightMap.SetTile(cellPos, null);
        
    }
}
