using UnityEngine;

public abstract class AgentAction
{
    public string ActionName;
    
    public int ActionLevel;
    public int maxActionLevel;

    public float ActionExperience;
    public float ExperienceNeededToLevelUp;

    public AgentResource ResourceToUse;
    public int ResourceCost;


    //Targeting Variables
    public TargetCategory target;
    public int Range; //Anything for melee would be 1
    public int TargetCount;


    protected AgentAction(string name, int startingLevel, int maxLevel, float expLevelUp)
    {
        ActionName = name;
        ExperienceNeededToLevelUp = expLevelUp;
        ActionLevel = startingLevel;
        maxActionLevel = maxLevel;

        //Temporary
        //Maybe change to a dict so it looks like -> ResourcesToUse("ActionPoint" : 1, "SpellSlot" : 2)
        ResourceToUse = new ActionPoint();
        ResourceCost = 1;
    }

    public abstract void Action(Agent ActionOwner);

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

    public void UseResource(Agent ActionOwner, AgentResource resource, int actioncost)
    {
        AgentResource usedresource = null;

        foreach (AgentResource usableResource in ActionOwner.allResources)
        {
            if (resource.ResourceName == usableResource.ResourceName)
            {
                usedresource = usableResource;
            }
        }
        if (usedresource == null)
        {
            Debug.Log("No resource found");
        }


        if (usedresource.currentAmount > 0)
        {
            int newAmount = usedresource.currentAmount - actioncost;
            if (newAmount >= 0)
            {
                usedresource.currentAmount = newAmount;
            }
            else
            {
                Debug.Log("Agent does not have enough resource");
            }
        }
        else
        {
            Debug.Log("Agent does not have enough resource");
        }

    }

}
