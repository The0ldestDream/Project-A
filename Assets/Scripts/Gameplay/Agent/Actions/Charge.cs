using UnityEngine;

public class Charge : AgentAction
{
    public Charge(int startingLevel, float expLevelUp) : base("Charge", startingLevel, 99, expLevelUp)
    {
        Range = 5;
        shape = TargetShape.Line;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        int modifier = CalculateModifier(ActionOwner);

        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Charge has been Used");
            ActionOwner.controller.MoveTo(ActionTarget.tile);
            if (ActionTarget.agent != null)
            {
                ActionTarget.agent.DealDamage(1 + modifier);
            }
        }

    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        // Calculate the Modifier I want the Move to benefit the most from
        Stat strength = ActionOwner.statSheet.GetStat("Strength");
        int modifier = Mathf.RoundToInt((float)(strength.currentValue * 0.5));

        return modifier;
    }
}
