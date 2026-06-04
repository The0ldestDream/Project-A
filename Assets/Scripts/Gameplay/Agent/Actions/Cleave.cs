using UnityEngine;
using System.Collections.Generic;
public class Cleave : AgentAction
{
    public Cleave(int startingLevel, float expLevelUp) : base("Cleave", startingLevel, 99, expLevelUp)
    {
        baseDamage = 3;

        Range = 1;
        Width = 3;
        shape = TargetShape.Sweep;
        target = TargetCategory.Tile;

        Scaling.Add(StatNames.Strength, 0.4f);

    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, ActionTarget.tile);

        float scalingModifier = CalculateScalingDamage(ActionOwner);
        float BaseActionDamage = baseDamage + scalingModifier;

        
        DamageContext dContext = new DamageContext();

        dContext.Attacker = ActionOwner;



        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            List<GridCell> affectedCells = shapeHelper.FindCellsInfront(grid, ActionOwner.gridPos, ActionTarget.tile, Range, Width);

            foreach (GridCell cell in affectedCells)
            {
                DamageInfo tempInfo = new DamageInfo();
                tempInfo.Attacker = ActionOwner;
                tempInfo.DamageNumbers[DamageType.Slashing] += (int)BaseActionDamage;

                Target TargetedCell = new Target(cell, cell.AgentOnTile);
                dContext.Defender = TargetedCell;

                List<Contribution> tempContributions = ActionOwner.BuildDamage(ActionOwner, dContext);
                tempInfo = MergeContributions(tempInfo, tempContributions);

                DamageInfo tempResolvedInfo = cell.damageable.ResolveDamage(tempInfo);

                cell.damageable.DealDamage(tempResolvedInfo);

            }
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
