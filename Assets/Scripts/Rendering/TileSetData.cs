using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileSetData", menuName = "Scriptable Objects/TileSetData")]
public class TileSetData : ScriptableObject
{
    public Tile floor;
    public Tile wall;
    public Tile door;
    public Tile stairs;
    public Tile spawn; //For Testing Purposes

}
