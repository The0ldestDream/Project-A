using UnityEngine;
using System.Collections.Generic;

public abstract class AgentClass
{
    public string ClassName;

    public int ClassLevel;

    public float ClassExperience;
    public float ExperienceNeededToLevelUp;

    public List<AgentAction> ClassActions = new List<AgentAction>();


    protected AgentClass(string name, int startingLevel, float expLevelUp)
    {
        ClassName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        ClassLevel = startingLevel;
    }




    public virtual void GainExperience(float ExperienceGained)
    {

        ClassExperience += ExperienceGained;

        if (ClassExperience >= ExperienceNeededToLevelUp)
        {
            ClassLevel += 1;
            ClassExperience = 0; // May need to change if the player, for some random reason, gains a lot of experience that may level them more than once
            ExperienceNeededToLevelUp *= 1.2f;
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
        foreach (AgentAction action in ClassActions)
        {
            OwnerAgent.allActions.Add(action);
        }
    }
}
