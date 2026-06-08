using UnityEngine;
using System.Collections.Generic;
public class AgentDescription
{
    public RaceType Race;
    public ClassType Class;
    public int ClassLevel;
    public AgentAlignment Alignment;

    public List<AgentTrait> agentTraits; // We won't need this for now

    public AgentDescription(RaceType agentRace, ClassType agentClass, AgentAlignment agentAlignment)
    {
        Race = agentRace;
        Class = agentClass;
        Alignment = agentAlignment;
    }
}
