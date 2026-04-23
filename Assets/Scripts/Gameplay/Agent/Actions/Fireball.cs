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
        

        int modifier = CalculateModifier(ActionOwner);
        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Fireball has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinRadius(grid, ActionTarget.tile, Radius);

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
        Stat intelligence = ActionOwner.statSheet.GetStat("Intelligence");
        int modifier = Mathf.RoundToInt((float)(intelligence.currentValue * 0.5));

        return modifier;
    }
}
