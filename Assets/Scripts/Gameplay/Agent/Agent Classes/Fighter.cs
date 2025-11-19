using UnityEngine;

public class Fighter : AgentClass
{
    
    public Fighter(int startingLevel, float expLevelUp) : base("Fighter", startingLevel, expLevelUp)
    {
        //Add starting actions
        ClassActions.Add(new Stab(1, 0));


    }


    public override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }

}
