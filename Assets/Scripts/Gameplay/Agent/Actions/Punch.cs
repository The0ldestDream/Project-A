using UnityEngine;

public class Punch : AgentAction
{
    public Punch(int startingLevel, float expLevelUp) : base("Punch", startingLevel, 99, expLevelUp)
    {
        Range = 20;
        target = TargetCategory.Agent;
    }

    public override void Action(Agent ActionOwner, Target target)
    {
        
        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Punch has been Used");
            target.agent.DealDamage(1 + modifier);
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
