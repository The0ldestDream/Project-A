using UnityEngine;
using System.Collections.Generic;


public abstract class AgentTrait
{
    public string TraitName;

    public int TraitLevel;
    public int TraitExperience;
    public int ExperienceNeededToLevelUp;

    public AgentTrait(string name, int startingLevel, int expLevelUp)
    {
        TraitName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        TraitLevel = startingLevel;
    }

    public abstract int StatModifier(Stat stat);

    public abstract List<Contribution> DamageModifier(DamageContext damageContext);

    public void GainExperience(int experienceGained)
    {
        TraitExperience += experienceGained;

        if (TraitExperience >= ExperienceNeededToLevelUp)
        {
            TraitLevel += 1;
            TraitExperience = 0;
            float NewExpCap = ExperienceNeededToLevelUp * 0.3f;
            ExperienceNeededToLevelUp += (int)NewExpCap;
            TraitLevelUp();
        }
    }


    public abstract void TraitLevelUp();

    public abstract void TraitUniqueLevelUp();

}
