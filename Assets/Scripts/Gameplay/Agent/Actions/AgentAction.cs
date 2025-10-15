using UnityEngine;

public abstract class AgentAction
{
    public string ActionName;
    
    public int ActionLevel;
    public int maxActionLevel;
    public float ActionExperience;
    public float ExperienceNeededToLevelUp;

    public AgentAction(string name, int startingLevel, int maxLevel, float expLevelUp)
    {
        ActionName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        ActionLevel = startingLevel;
        maxActionLevel = maxLevel;

    }

    public abstract void Action();

    public void GainExperience(float experienceGained)
    {
        ActionExperience += experienceGained;

        if (ActionExperience >= ExperienceNeededToLevelUp)
        {
            ActionLevel += 1;
            ActionExperience = 0;
            ExperienceNeededToLevelUp *= 1.2f;
            ActionLevelUp();
        }
    }


    public virtual void ActionLevelUp()
    {

    }

    public abstract void ActionUniqueLevelUp();

}
