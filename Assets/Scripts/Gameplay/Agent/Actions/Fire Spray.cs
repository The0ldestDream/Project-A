using UnityEngine;
using System.Collections.Generic;
public class FireSpray : AgentAction
{
    public FireSpray(int startingLevel, int expLevelUp) : base("Fire Spray", startingLevel, 99, expLevelUp)
    {
        baseDamage = 2;

        Range = 15;
        Width = 2;
        shape = TargetShape.Cone;
        target = TargetCategory.Tile;

        Scaling.Add(StatNames.Intelligence, 0.3f);
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
            Debug.Log("Fire Spray has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinCone(grid, ActionOwner.gridPos, ActionTarget.tile, Range, Width);

            foreach (GridCell cell in affectedCells)
            {
                if (!CheckIfCellIsValid(cell))
                {
                    continue;
                }
                DamageInfo tempInfo = new DamageInfo();
                tempInfo.Attacker = ActionOwner;
                tempInfo.DamageNumbers[DamageType.Fire] += (int)BaseActionDamage;

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
