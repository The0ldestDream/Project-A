using UnityEngine;

public class Sorcerer : AgentClass
{
    public Sorcerer(int startingLevel, float expLevelUp) : base("Sorcerer", startingLevel, expLevelUp)
    {
        //Add starting actions
        ClassActions.Add(new Fireball(1, 0));
        ClassActions.Add(new LightningBolt(1, 0));
        ClassActions.Add(new FireSpray(1, 0));

    }

    public override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
