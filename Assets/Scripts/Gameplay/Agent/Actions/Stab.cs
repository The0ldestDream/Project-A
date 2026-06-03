using UnityEngine;
using System.Collections.Generic;
public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {
        baseDamage = 2;
        Range = 1;
        shape = TargetShape.Single;
        target = TargetCategory.Agent;

        //Scaling Values
        Scaling.Add(StatNames.Strength, 0.2f);
        Scaling.Add(StatNames.Dexterity, 0.5f);

    }

    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, target.tile);
        int scalingModifier = CalculateScalingDamage(ActionOwner);
        

        DamageInfo dInfo = new DamageInfo();
        DamageContext dContext = new DamageContext();

        dInfo.Attacker = ActionOwner;
        dContext.Attacker = ActionOwner;

        

        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Stab has been Used");

            dContext.Defender = target;
            //Build the base action damage 
            float BaseActionDamage = baseDamage + scalingModifier;
            dInfo.DamageNumbers[DamageType.Piercing] += (int)BaseActionDamage;

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

    public override int CalculateScalingDamage(Agent ActionOwner)
    {
        int dexValue = ActionOwner.statSheet.GetStatValue("Dexterity");
        int strengthValue = ActionOwner.statSheet.GetStatValue("Strength");

        float dexScaling = GetScalingValue(StatNames.Dexterity);
        float strengthScaling = GetScalingValue(StatNames.Strength);

        int modifier = Mathf.RoundToInt((float)(dexValue * dexScaling + strengthValue * strengthScaling));

        return modifier; 
    }
}
