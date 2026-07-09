using UnityEngine;

public abstract class ConsumableItem : Item
{


    public void UseItem(Agent agent)
    {
        foreach (ItemEffect effect in itemEffects) 
        {

            if (effect.trigger == EffectTrigger.OnUsed) // We just want to look for OnUsed events
            {
                effect.TriggerEffect(agent);
            }    

        }
    }

}
