using UnityEngine;

public class Punch : AgentAction
{
    public Punch(int startingLevel, float expLevelUp) : base("Punch", startingLevel, 99, expLevelUp)
    {
        Range = 1;
        shape = TargetShape.Single;
        target = TargetCategory.Agent;
    }

    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, target.tile);

        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Punch has been Used");
            target.tile.damageable.DealDamage(1 + modifier);
        }

    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        // Calculate the Modifier I want the Move to benefit the most from
        int strengthValue = ActionOwner.statSheet.GetStatValue("Strength");
        int modifier = Mathf.RoundToInt((float)(strengthValue * 0.5));

        return modifier;
    }
}
