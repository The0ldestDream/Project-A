using UnityEngine;

public class Stab : AgentAction
{
    public Stab(int startingLevel, float expLevelUp) : base("Stab", startingLevel, 99, expLevelUp)
    {

    }

    public override void Action()
    {
        Debug.Log("Stab has been Used");
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
