using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {
        Range = 20;
        shape = TargetShape.Single;
        target = TargetCategory.Agent;
    }

    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        
        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Stab has been Used");
            target.agent.DealDamage(1 + modifier);
        }




        
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
