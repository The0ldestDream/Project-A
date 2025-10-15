using UnityEngine;

public class Fighter : AgentClass
{
    
    public Fighter(string name, int startingLevel, float expLevelUp) : base(name, startingLevel, expLevelUp)
    {
        //Add starting actions
        ClassActions.Add(new Stab("Stab", 1, 99, 0));


    }


    public override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }

}
