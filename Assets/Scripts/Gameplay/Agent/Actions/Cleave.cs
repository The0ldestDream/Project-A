using UnityEngine;
using System.Collections.Generic;
public class Cleave : AgentAction
{
    public Cleave(int startingLevel, float expLevelUp) : base("Cleave", startingLevel, 99, expLevelUp)
    {
        Range = 2;
        Width = 3;
        shape = TargetShape.Sweep;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {

        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            List<GridCell> affectedCells = shapeHelper.FindCellsInfront(grid, ActionOwner.gridPos, ActionTarget.tile, Range, Width);

            int modifier = CalculateModifier(ActionOwner);

            foreach (GridCell cell in affectedCells)
            {
                cell.AgentOnTile.DealDamage(1 + modifier);

            }
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
