using UnityEngine;

public class Punch : AgentAction
{
    public Punch(string name, int startingLevel, int maxLevel, float expLevelUp) : base(name, startingLevel, maxLevel, expLevelUp)
    {

    }

    public override void Action()
    {
        Debug.Log("Punch has been Used");
    }

    public override void ActionUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
