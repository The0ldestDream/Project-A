using UnityEngine;

public abstract class AgentTrait
{
    public string TraitName;

    public int TraitLevel;
    public float TraitExperience;
    public float ExperienceNeededToLevelUp;

    public AgentTrait(string name, int startingLevel, float expLevelUp)
    {
        TraitName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        TraitLevel = startingLevel;
    }

    public abstract void Trait();


    public void GainExperience(float experienceGained)
    {
        TraitExperience += experienceGained;

        if (TraitExperience >= ExperienceNeededToLevelUp)
        {
            TraitLevel += 1;
            TraitExperience = 0;
            ExperienceNeededToLevelUp *= 1.2f;
            TraitLevelUp();
        }
    }


    public abstract void TraitLevelUp();

    public abstract void TraitUniqueLevelUp();

}
