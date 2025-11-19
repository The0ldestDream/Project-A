using UnityEngine;

public class Human : AgentRace
{
    public Human() : base("Human")
    {
        AddActions();
        AddTraits();
    }

    public override void AddActions()
    {
        RaceActions.Add(new Punch(1, 1));
    }

    public override void AddTraits()
    {
        RaceTraits.Add(new FormidableSolider(1, 1));
    }

}
