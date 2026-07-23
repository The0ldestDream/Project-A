using UnityEngine;

public class HealthPotion : ConsumableItem
{
    
    public HealthPotion()
    {
        itemEffects.Add(new HealEffect(EffectTrigger.OnUsed));

        ItemName = "Health Potion";
    }
}
