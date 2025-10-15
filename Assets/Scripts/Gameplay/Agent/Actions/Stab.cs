using UnityEngine;

public class Stab : AgentAction
{
    public Stab(string name, int startingLevel, int maxLevel, float expLevelUp) : base(name, startingLevel, maxLevel, expLevelUp)
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
