using UnityEngine;
using System.Collections.Generic;
public class LightningBolt : AgentAction
{
    public LightningBolt(int startingLevel, float expLevelUp) : base("Lightning Bolt", startingLevel, 99, expLevelUp)
    {
        Range = 10;
        shape = TargetShape.Line;
        target = TargetCategory.Tile;
    }
    public override void Action(Agent ActionOwner, Target target, GridSystem grid)
    {
        ActionOwner.FindDirection(ActionOwner.gridPos, target.tile);


        if (UseResource(ActionOwner, ResourceToUse, ResourceCost))
        {
            Debug.Log("Lightning Bolt has been Used");

            List<GridCell> affectedCells = shapeHelper.FindCellsWithinLine(grid, ActionOwner.gridPos, target.tile);

            foreach (GridCell cell in affectedCells)
            {
                int modifier = CalculateModifier(ActionOwner);
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
        // Calculate the Modifier I want the Move to benefit the most from
        int intelligenceValue = ActionOwner.statSheet.GetStatValue("Intelligence");
        int modifier = Mathf.RoundToInt((float)(intelligenceValue * 0.5));

        return modifier;
    }
}
