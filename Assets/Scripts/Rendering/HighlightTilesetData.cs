using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "HighlightTilesetData", menuName = "Scriptable Objects/HighlightTilesetData")]
public class HighlightTilesetData : ScriptableObject
{
    public Tile TargetTile;
    public Tile ClearTile = null;
}
