using UnityEngine;

public abstract class ItemEffect
{
    public EffectTrigger trigger;

    public ItemEffect(EffectTrigger ChosenTrigger)
    {
        trigger = ChosenTrigger;
    }

    public abstract void TriggerEffect(Agent agent);
}
