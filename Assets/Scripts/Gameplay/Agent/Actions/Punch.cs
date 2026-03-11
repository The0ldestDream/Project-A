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
        Debug.Log("Punch has been Used");
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
        // Calculate the Modifier I want the Move to benefit the most from
        Stat strength = ActionOwner.statSheet.GetStat("Strength");
        int modifier = Mathf.RoundToInt((float)(strength.currentValue * 0.5));

        return modifier;
    }
}
