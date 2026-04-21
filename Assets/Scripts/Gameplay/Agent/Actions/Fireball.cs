using UnityEngine;
using System.Collections.Generic;

public class Fireball : AgentAction
{
    public Fireball(int startingLevel, float expLevelUp) : base("Fireball", startingLevel, 1, expLevelUp)
    {
        Range = 8;
        Radius = 2;
        shape = TargetShape.Single;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        List<GridCell> affectedCells = shapeHelper.FindCellsWithinRadius(grid, ActionTarget.tile, Radius);
        

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
