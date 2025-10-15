using UnityEngine;

public class Human : AgentRace
{
    public Human(string Name) : base(Name)
    {

    }

    public override void AddActions()
    {
        RaceActions.Add(new Punch("Punch", 1, 99, 1));
    }

    public override void AddTraits()
    {
        RaceTraits.Add(new FormidableSolider("Formidable Solider", 1, 1));
    }

}
