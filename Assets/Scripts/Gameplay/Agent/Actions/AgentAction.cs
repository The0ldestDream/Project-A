using UnityEngine;
using System.Collections.Generic;
public abstract class AgentAction
{
    public string ActionName;
    
    public int ActionLevel;
    public int maxActionLevel;

    public float ActionExperience;
    public float ExperienceNeededToLevelUp;

    public AgentResource ResourceToUse;
    public int ResourceCost;

    //Action Information
    public int baseDamage;
    public Dictionary<StatNames, float> Scaling = new Dictionary<StatNames, float>();
    DamageType damageType;

    //Targeting Variables
    public TargetShape shape;
    public TargetCategory target;
    public int Range; //Anything for melee would be 1
    public int Width; //Anything that uses a cone or thicker line
    public int Radius; //For Circular shapes
    public int TargetCount;

    //Helper
    public ShapeHelper shapeHelper = new ShapeHelper();

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

    public abstract void Action(Agent ActionOwner, Target ActionTarget, GridSystem grid);

    public abstract int CalculateScalingDamage(Agent ActionOwner);

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

    public bool UseResource(Agent ActionOwner, AgentResource resource, int actioncost)
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
                return true;
            }
            else
            {
                Debug.Log("Agent does not have enough resource");
                return false;
            }
        }
        else
        {
            Debug.Log("Agent does not have enough resource");
            return false;
        }

    }

    public float GetScalingValue(StatNames stat)
    {
        Scaling.TryGetValue(stat, out float value);
        return value;
    }

    public float GetDamageModifiers(Agent agent, DamageContext context)
    {
        StatSheet sheet = agent.statSheet;
        float value = 0f;


        foreach (AgentTrait trait in sheet.allTraits)
        {
            value += trait.DamageModifier(context);
        }

        return value;
    }
}
