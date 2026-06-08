using UnityEngine;

public class Fighter : AgentClass
{
    
    public Fighter(int startingLevel, int expLevelUp) : base("Fighter", startingLevel, expLevelUp)
    {
        //Add starting actions
        ClassActions.Add(new Stab(1, 0));
        

    }


    public override void OnLevelUp()
    {
        Debug.Log("Fighter is now Leveled Up to " + ClassLevel);

        if (ClassLevel == 2)
        {
            ClassActions.Add(new Charge(1, 0));
            ClassActions.Add(new Cleave(1, 0));

            
        }

    }

}
