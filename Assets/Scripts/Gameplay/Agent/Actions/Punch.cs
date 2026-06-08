using UnityEngine;
using System.Collections.Generic;
public class Punch : AgentAction
{
    public Punch(int startingLevel, int expLevelUp) : base("Punch", startingLevel, 99, expLevelUp)
    {
        baseDamage = 3;

        Range = 1;
        shape = TargetShape.Single;
        target = TargetCategory.Agent;
    }

    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, target.tile);

        float scalingModifier = CalculateScalingDamage(ActionOwner);


        DamageInfo dInfo = new DamageInfo();
        DamageContext dContext = new DamageContext();

        dInfo.Attacker = ActionOwner;
        dContext.Attacker = ActionOwner;


        
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Punch has been Used");
            dContext.Defender = target;
            //Build the base action damage 
            float BaseActionDamage = baseDamage + scalingModifier;
            dInfo.DamageNumbers[DamageType.Bludgeoning] += (int)BaseActionDamage;

            //Now we get contributions from other sources
            List<Contribution> contributions = ActionOwner.BuildDamage(ActionOwner, dContext);
            dInfo = MergeContributions(dInfo, contributions);

            //A Resolve Damage action stage here
            DamageInfo ResolvedInfo = target.tile.damageable.ResolveDamage(dInfo);

            //Apply Damage after Resolution
            target.tile.damageable.DealDamage(ResolvedInfo);
        }

    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override float CalculateScalingDamage(Agent ActionOwner)
    {
        // Calculate the Modifier I want the Move to benefit the most from
        int strengthValue = ActionOwner.statSheet.GetStatValue("Strength");

        float strengthScaling = GetScalingValue(StatNames.Strength);

        float modifier = strengthValue * strengthScaling;

        return modifier;
    }
}
