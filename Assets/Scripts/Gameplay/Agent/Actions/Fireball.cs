using UnityEngine;
using System.Collections.Generic;

public class Fireball : AgentAction
{
    public Fireball(int startingLevel, float expLevelUp) : base("Fireball", startingLevel, 1, expLevelUp)
    {
        Range = 8;
        Radius = 2;
        shape = TargetShape.Radius;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, ActionTarget.tile);


        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Fireball has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinRadius(grid, ActionTarget.tile, Radius);

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
