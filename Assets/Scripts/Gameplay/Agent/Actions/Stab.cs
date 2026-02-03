using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {

    }

    public override void Action(Agent ActionOwner)
    {
        Debug.Log("Stab has been Used");
        UseResource(ActionOwner, ResourceToUse, ResourceCost);
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
