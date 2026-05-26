using UnityEngine;

public class Lunge : AgentAction
{
    public Lunge(int startingLevel, float expLevelUp) : base("Lunge", startingLevel, 1, expLevelUp)
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

    public override int CalculateScalingDamage(Agent ActionOwner)
    {
        throw new System.NotImplementedException();
    }
}
