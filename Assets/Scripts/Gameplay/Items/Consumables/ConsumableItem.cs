using UnityEngine;

public abstract class ConsumableItem : Item
{


    public override bool Activate(Agent agent)
    {
        foreach (ItemEffect effect in itemEffects)
        {

            if (effect.trigger == EffectTrigger.OnUsed) // We just want to look for OnUsed events
            {
                effect.TriggerEffect(agent);
            }
            
        }

        if (!Reusable)
        {
            return true; // item is consumed
        }
        else 
        {
            return false;
        }
    }

}
