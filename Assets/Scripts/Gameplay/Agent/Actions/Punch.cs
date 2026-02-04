using UnityEngine;

public class Punch : AgentAction
{
    public Punch(int startingLevel, float expLevelUp) : base("Punch", startingLevel, 99, expLevelUp)
    {

    }

    public override void Action(Agent ActionOwner)
    {
        Debug.Log("Punch has been Used");
        UseResource(ActionOwner, ResourceToUse, ResourceCost);
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
