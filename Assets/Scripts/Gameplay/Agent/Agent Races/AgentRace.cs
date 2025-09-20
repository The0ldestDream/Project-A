using UnityEngine;
using System.Collections.Generic;
public abstract class AgentRace
{

    public string RaceName;

    public List<AgentAction> RaceActions;
    public List<AgentTrait> RaceTraits;

    public AgentRace(string Name)
    {
        RaceName = Name;
    }

    public abstract void AddActions();

    public abstract void AddTraits();

    public void AddAction(AgentAction newAction)
    {
        RaceActions.Add(newAction);
    }

    public void AddTrait(AgentTrait newTrait)
    {
        RaceTraits.Add(newTrait);
    }

}
