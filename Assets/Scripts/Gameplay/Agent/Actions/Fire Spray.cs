using UnityEngine;
using System.Collections.Generic;
public class FireSpray : AgentAction
{
    public FireSpray(int startingLevel, float expLevelUp) : base("Fire Spray", startingLevel, 99, expLevelUp)
    {
        Range = 15;
        Width = 2;
        shape = TargetShape.Cone;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, ActionTarget.tile);


        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Fire Spray has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinCone(grid, ActionOwner.gridPos, ActionTarget.tile, Range, Width);

            foreach (GridCell cell in affectedCells)
            {
                cell.damageable.DealDamage(1 + modifier);
            }
        }
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        int intelligenceValue = ActionOwner.statSheet.GetStatValue("Intelligence");
        int modifier = Mathf.RoundToInt((float)(intelligenceValue * 0.5));

        return modifier;
    }
}
