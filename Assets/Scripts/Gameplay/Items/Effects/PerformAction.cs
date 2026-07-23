using UnityEngine;

public class PerformAction : ItemEffect
{

    public PerformAction(EffectTrigger ChosenTrigger) : base(ChosenTrigger)
    {

    }

    public AgentAction ActionToUse;

    public override void TriggerEffect(Agent agent)
    {

    }
}
