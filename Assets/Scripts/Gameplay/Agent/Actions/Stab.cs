using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {
        Range = 20;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target target)
    {
        Debug.Log("Stab has been Used");
        UseResource(ActionOwner, ResourceToUse, ResourceCost);


        //Temp 


    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
