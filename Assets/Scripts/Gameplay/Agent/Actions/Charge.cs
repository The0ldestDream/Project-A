using UnityEngine;
using System.Collections.Generic;
public class Charge : AgentAction
{
    public Charge(int startingLevel, float expLevelUp) : base("Charge", startingLevel, 99, expLevelUp)
    {
        Range = 5;
        shape = TargetShape.Line;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        int modifier = CalculateModifier(ActionOwner);

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
                    cell.damageable.DealDamage(1 + modifier);
                }

                ActionTarget.tile.damageable.DealDamage(1 + modifier);
            }
            else
            {
                ActionOwner.controller.MoveTo(ActionTarget.tile);
                foreach (GridCell cell in affectedCells)
                {
                    cell.damageable.DealDamage(1 + modifier);
                }
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
        int strengthValue = ActionOwner.statSheet.GetStatValue("Strength");
        int modifier = Mathf.RoundToInt((float)(strengthValue * 0.5));

        return modifier;
    }
}
