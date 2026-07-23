using UnityEngine;

public class EquippableItem : Item
{
    public EquipmentSlot slot;


    public EquippableItem(ItemDescription itemDescription)
    {
        ItemName = itemDescription.ItemName;
        itemEffects = itemDescription.itemEffects;
    }

    public override bool Activate(Agent agent)
    {
        throw new System.NotImplementedException();
    }

    public void Equip(Agent agent)
    {
        // Equip to equipment slot


        // OnEquipped Triggers
        foreach (ItemEffect effect in itemEffects)
        {

            if (effect.trigger == EffectTrigger.OnEquipped) // We just want to look for OnUsed events
            {
                effect.TriggerEffect(agent);
            }

        }
    }


    public void Unequip(Agent agent)
    {
        // Equip to equipment slot


        // OnEquipped Triggers
        foreach (ItemEffect effect in itemEffects)
        {

            if (effect.trigger == EffectTrigger.OnUnequipped) // We just want to look for OnUsed events
            {
                effect.TriggerEffect(agent);
            }

        }
    }

}
