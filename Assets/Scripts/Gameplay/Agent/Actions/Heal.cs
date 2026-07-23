using UnityEngine;

public class Heal : AgentAction
{

    public Heal(int startingLevel, int expLevelUp) : base("Heal", startingLevel, 99, expLevelUp)
    {
        
        Range = 5;
        shape = TargetShape.Single;
        target = TargetCategory.Tile;

    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        AgentResource health = ActionTarget.agent.allResources.Find(x => x.ResourceName == "Health");

        health.AdjustValue(5);
        
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
