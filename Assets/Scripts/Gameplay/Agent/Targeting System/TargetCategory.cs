using UnityEngine;

public enum TargetCategory
{
    Self, 
    Agent,
    Tile,
    Direction // For actions that may come out of the agent in a certain shape. Shape would be defined within the action or maybe in another script.
}
