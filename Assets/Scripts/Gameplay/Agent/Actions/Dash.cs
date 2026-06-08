using UnityEngine;

public class Dash : AgentAction
{
    public Dash(int startingLevel, int expLevelUp) : base("Dash", startingLevel, 1, expLevelUp)
    {

    }


    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        throw new System.NotImplementedException();
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override float CalculateScalingDamage(Agent ActionOwner)
    {
        throw new System.NotImplementedException();
    }
}
