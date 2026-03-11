using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {
        Range = 20;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target target)
    {
        Debug.Log("Stab has been Used");
        UseResource(ActionOwner, ResourceToUse, ResourceCost);

        int modifier = CalculateModifier(ActionOwner);


        target.agent.DealDamage(1 + modifier);

    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        Stat dex = ActionOwner.statSheet.GetStat("Dexterity");
        Stat strength = ActionOwner.statSheet.GetStat("Strength");
        int modifier = Mathf.RoundToInt((float)(dex.currentValue * 0.5 + strength.currentValue * 0.2));

        return modifier; 
    }
}
