using UnityEngine;

public class Lunge : AgentAction
{
    public Lunge(int startingLevel, int expLevelUp) : base("Lunge", startingLevel, 1, expLevelUp)
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
