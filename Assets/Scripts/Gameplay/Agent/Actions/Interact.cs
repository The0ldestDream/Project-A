using UnityEngine;

public class Interact : AgentAction
{
    public Interact(int startingLevel, float expLevelUp) : base("Interact", startingLevel, 1, expLevelUp)
    {
        Range = 1;
        target = TargetCategory.Object;
    }

    public override void Action(Agent ActionOwner, Target ActionTarget)
    {
        ActionTarget.tile.interactable.Interact(ActionOwner);
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
