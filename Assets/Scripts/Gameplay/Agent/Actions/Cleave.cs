using UnityEngine;

public class Cleave : AgentAction
{
    public Cleave(int startingLevel, float expLevelUp) : base("Cleave", startingLevel, 99, expLevelUp)
    {
        Range = 2;
        shape = TargetShape.Cone;
        target = TargetCategory.Agent;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        throw new System.NotImplementedException();
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        throw new System.NotImplementedException();
    }
}
