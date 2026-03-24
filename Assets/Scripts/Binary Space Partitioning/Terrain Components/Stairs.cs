using UnityEngine;

public class Stairs : IInteractable
{
    GridCell gridPosition;
    ObjectType objectType = ObjectType.Stairs;

    public Stairs(GridCell location)
    {
        gridPosition = location;

        gridPosition.interactable = this;
        
    }

    public void DeleteStairs()
    {
        gridPosition.interactable = null;
    }

    public void Interact(Agent agent)
    {
        //Trigger Next Level Generation
    }
}
