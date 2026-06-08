using UnityEngine;
using System.Collections.Generic;

public abstract class AgentClass
{
    public string ClassName;

    public int ClassLevel;

    public int ClassExperience;
    public int ExperienceNeededToLevelUp;

    public List<AgentAction> ClassActions = new List<AgentAction>();


    protected AgentClass(string name, int startingLevel, int expLevelUp)
    {
        ClassName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        ClassLevel = startingLevel;
    }




    public virtual void GainExperience(int ExperienceGained)
    {
        Debug.Log("Agent has gained " + ExperienceGained);
        ClassExperience += ExperienceGained;

        if (ClassExperience >= ExperienceNeededToLevelUp)
        {
            ClassLevel += 1;
            ClassExperience = 0; // May need to change if the player, for some random reason, gains a lot of experience that may level them more than once
            float NewExpCap = ExperienceNeededToLevelUp * 0.3f;
            ExperienceNeededToLevelUp += (int)NewExpCap;

            Debug.Log("Agent has levelled up to " + ClassLevel);
            OnLevelUp();
        }


    }


    public abstract void OnLevelUp();

    public void AddAction(AgentAction newAction)
    {

        ClassActions.Add(newAction);

    }


    public void GiveActions(Agent OwnerAgent)
    {
        List<AgentAction> newActions = new List<AgentAction>();

        foreach (AgentAction classAction in ClassActions)
        {
            AgentAction agentAction = OwnerAgent.allActions.Find(x => x.ActionName == classAction.ActionName);

            if (agentAction == null)
            {
                OwnerAgent.allActions.Add(classAction);
            }
        }
    }
}
