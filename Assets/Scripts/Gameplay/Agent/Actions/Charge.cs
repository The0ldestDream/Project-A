using UnityEngine;
using System.Collections.Generic;
public class Charge : AgentAction
{
    public Charge(int startingLevel, int expLevelUp) : base("Charge", startingLevel, 99, expLevelUp)
    {
        baseDamage = 1;
        Range = 5;
        shape = TargetShape.Line;
        target = TargetCategory.Tile;


        Scaling.Add(StatNames.Strength, 0.3f);
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        float scalingModifier = CalculateScalingDamage(ActionOwner);
        float BaseActionDamage = baseDamage + scalingModifier;

        DamageContext dContext = new DamageContext();

        
        dContext.Attacker = ActionOwner;


        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Charge has been Used");


            List<GridCell> affectedCells = shapeHelper.FindCellsWithinLine(grid, ActionOwner.gridPos, ActionTarget.tile);

            if (ActionTarget.agent != null)
            {
                GridCell closestCellToTarget = shapeHelper.FindFurthestCellInList(affectedCells, ActionOwner.gridPos);
                ActionOwner.controller.MoveTo(closestCellToTarget);

                foreach (GridCell cell in affectedCells)
                {
                    if (!CheckIfCellIsValid(cell))
                    {
                        continue;
                    }
                    DamageInfo tempInfo = new DamageInfo();
                    tempInfo.Attacker = ActionOwner;
                    tempInfo.DamageNumbers[DamageType.Piercing] += (int)BaseActionDamage;

                    Target TargetedCell = new Target(cell, cell.AgentOnTile);
                    dContext.Defender = TargetedCell;

                    List<Contribution> tempContributions = ActionOwner.BuildDamage(ActionOwner, dContext);
                    tempInfo = MergeContributions(tempInfo, tempContributions);

                    DamageInfo tempResolvedInfo = cell.damageable.ResolveDamage(tempInfo);

                    cell.damageable.DealDamage(tempResolvedInfo);
                }

                DamageInfo dInfo = new DamageInfo();
                dInfo.Attacker = ActionOwner;
                dInfo.DamageNumbers[DamageType.Piercing] += (int)BaseActionDamage;
                List<Contribution> contributions = ActionOwner.BuildDamage(ActionOwner, dContext);
                dInfo = MergeContributions(dInfo, contributions);

                DamageInfo ResolvedInfo = ActionTarget.tile.damageable.ResolveDamage(dInfo);

                ActionTarget.tile.damageable.DealDamage(ResolvedInfo);
            }
            else
            {
                ActionOwner.controller.MoveTo(ActionTarget.tile);
                foreach (GridCell cell in affectedCells)
                {
                    if (!CheckIfCellIsValid(cell))
                    {
                        continue;
                    }
                    DamageInfo tempInfo = new DamageInfo();
                    tempInfo.Attacker = ActionOwner;
                    tempInfo.DamageNumbers[DamageType.Piercing] += (int)BaseActionDamage;

                    Target TargetedCell = new Target(cell, cell.AgentOnTile);
                    dContext.Defender = TargetedCell;

                    List<Contribution> tempContributions = ActionOwner.BuildDamage(ActionOwner, dContext);
                    tempInfo = MergeContributions(tempInfo, tempContributions);

                    DamageInfo tempResolvedInfo = cell.damageable.ResolveDamage(tempInfo);

                    cell.damageable.DealDamage(tempResolvedInfo);
                }
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

        float modifier = (strengthValue * strengthScaling);

        return modifier;
    }
}
