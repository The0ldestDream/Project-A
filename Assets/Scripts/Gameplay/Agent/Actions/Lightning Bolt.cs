using UnityEngine;
using System.Collections.Generic;
public class LightningBolt : AgentAction
{
    public LightningBolt(int startingLevel, int expLevelUp) : base("Lightning Bolt", startingLevel, 99, expLevelUp)
    {
        baseDamage = 1;
        Range = 10;
        shape = TargetShape.Line;
        target = TargetCategory.Tile;

        Scaling.Add(StatNames.Intelligence, 0.5f);
    }
    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, target.tile);

        float scalingModifier = CalculateScalingDamage(ActionOwner);
        float BaseActionDamage = baseDamage + scalingModifier;
        DamageContext dContext = new DamageContext();
        dContext.Attacker = ActionOwner;

        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Lightning Bolt has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinLine(grid, ActionOwner.gridPos, target.tile);

            foreach (GridCell cell in affectedCells)
            {
                if (!CheckIfCellIsValid(cell))
                {
                    continue;
                }
                DamageInfo tempInfo = new DamageInfo();
                tempInfo.Attacker = ActionOwner;
                tempInfo.DamageNumbers[DamageType.Lightning] += (int)BaseActionDamage;

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
        int intelligenceValue = ActionOwner.statSheet.GetStatValue("Intelligence");
        float intelligenceScaling = GetScalingValue(StatNames.Intelligence);

        float modifier = (intelligenceValue * intelligenceScaling);

        return modifier;
    }
}
