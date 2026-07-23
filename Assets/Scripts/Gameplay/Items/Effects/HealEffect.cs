using UnityEngine;

public class HealEffect : ItemEffect
{

    public HealEffect(EffectTrigger ChosenTrigger) : base(ChosenTrigger)
    {

    }


    public override void TriggerEffect(Agent agent)
    {
        AgentResource health = agent.allResources.Find(x => x.ResourceName == "Health");
        Debug.Log("Health has gone from "+ health.currentAmount);
        health.AdjustValue(5);
        Debug.Log("to " + health.currentAmount);

    }
}
