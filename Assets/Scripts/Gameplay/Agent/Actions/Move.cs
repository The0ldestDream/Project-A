using UnityEngine;
using System.Collections;
public class Move : AgentAction
{

    public Move(int startingLevel, float expLevelUp) : base("Move", startingLevel, 99, expLevelUp)
    {
        Range = 5;
        target = TargetCategory.Tile;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget)
    {
        ActionOwner.controller.MoveTo(ActionTarget.tile);
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
