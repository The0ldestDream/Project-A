using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
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
            Debug.Log("Stab has been Used");
            target.tile.damageable.DealDamage(1 + modifier);
        }
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        int dexValue = ActionOwner.statSheet.GetStatValue("Dexterity");
        int strengthValue = ActionOwner.statSheet.GetStatValue("Strength");
        int modifier = Mathf.RoundToInt((float)(dexValue * 0.5 + strengthValue * 0.2));

        return modifier; 
    }
}
