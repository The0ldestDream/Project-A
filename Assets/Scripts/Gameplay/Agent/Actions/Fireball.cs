using UnityEngine;
using System.Collections.Generic;

public class Fireball : AgentAction
{
    public Fireball(int startingLevel, float expLevelUp) : base("Fireball", startingLevel, 1, expLevelUp)
    {
        baseDamage = 6;
        Range = 8;
        Radius = 2;
        shape = TargetShape.Radius;
        target = TargetCategory.Tile;

        Scaling.Add(StatNames.Intelligence, 0.6f);
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, ActionTarget.tile);
        
        DamageContext dContext = new DamageContext();
        
        dContext.Attacker = ActionOwner;
        float scalingModifier = CalculateScalingDamage(ActionOwner);
        float BaseActionDamage = baseDamage + scalingModifier;


        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Fireball has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinRadius(grid, ActionTarget.tile, Radius);

            foreach (GridCell cell in affectedCells)
            {
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
