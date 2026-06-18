using UnityEngine;
using System.Collections;
public class Move : AgentAction
{

    public Move(int startingLevel, int expLevelUp) : base("Move", startingLevel, 99, expLevelUp)
    {
        Range = 5;
        shape = TargetShape.Radius;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid)
    {
        
        if (ActionTarget.agent != null)
        {
            GridCell nearestcell = shapeHelper.NearestUnoccupiedCell(grid, ActionOwner.gridPos, ActionTarget.tile);

            if (nearestcell != null)
            {
                ActionOwner.controller.MoveTo(nearestcell);
            }
            
        }
        else
        {
            ActionOwner.controller.MoveTo(ActionTarget.tile);
        }
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override float CalculateScalingDamage(Agent ActionOwner)
    {
        throw new System.NotImplementedException();
    }
}
