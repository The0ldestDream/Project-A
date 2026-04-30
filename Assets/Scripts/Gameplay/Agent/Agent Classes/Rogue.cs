using UnityEngine;

public class Rogue : AgentClass
{

    public Rogue(int startingLevel, float expLevelUp) : base("Rogue", startingLevel, expLevelUp)
    {
        //Add starting actions
        ClassActions.Add(new Stab(1, 0));
        ClassActions.Add(new Charge(1, 0));
        ClassActions.Add(new Cleave(1, 0));

    }


    public override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }

    
}
