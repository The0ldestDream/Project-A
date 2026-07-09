using UnityEngine;

public abstract class ItemEffect
{
    public EffectTrigger trigger;

    public abstract void TriggerEffect(Agent agent);
}
