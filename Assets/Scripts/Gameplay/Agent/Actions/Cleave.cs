using UnityEngine;
using System.Collections.Generic;
public class Cleave : AgentAction
{
    public Cleave(int startingLevel, float expLevelUp) : base("Cleave", startingLevel, 99, expLevelUp)
    {
        Range = 2;
        Width = 3;
        shape = TargetShape.Cone;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
       List<GridCell> affectedCells = shapeHelper.FindCellsInfront(grid, ActionOwner.gridPos, ActionTarget.tile, Range, Width);

        foreach (GridCell cell in affectedCells)
        {
            Debug.Log("Cell at (" + cell.x + ", " + cell.y + ")");
        }

    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateModifier(Agent ActionOwner)
    {
        throw new System.NotImplementedException();
    }
}
