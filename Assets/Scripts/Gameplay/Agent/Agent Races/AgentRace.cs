using UnityEngine;
using System.Collections.Generic;
public abstract class AgentRace
{

    public string RaceName;

    public List<AgentAction> RaceActions = new List<AgentAction>();
    public List<AgentTrait> RaceTraits = new List<AgentTrait>();

    protected AgentRace(string Name)
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

    public void GiveActionsandTraits(Agent OwnerAgent)
    {
        foreach (AgentAction action in RaceActions)
        {
            OwnerAgent.allActions.Add(action);
        }

        foreach (AgentTrait trait in RaceTraits)
        {
            OwnerAgent.allTraits.Add(trait);
        }
    }
}
