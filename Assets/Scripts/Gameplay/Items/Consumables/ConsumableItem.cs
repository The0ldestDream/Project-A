using UnityEngine;

public class ConsumableItem : Item
{
    public ConsumableItem(ItemDescription itemDescription)
    {
        ItemName = itemDescription.ItemName;
        itemEffects = itemDescription.itemEffects;
        ID = itemDescription.ID;
    }

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
