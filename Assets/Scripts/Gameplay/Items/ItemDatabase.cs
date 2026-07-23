using UnityEngine;
using System.Collections.Generic;
public class ItemDatabase
{
    public Dictionary<ItemID, ItemDescription> Database = new Dictionary<ItemID, ItemDescription>();


    public ItemDatabase()
    {
        //Add the Items here
        AddItems();
    }

    private void AddItems()
    {
        Database.Add(ItemID.HealthPotion, new ItemDescription(ItemType.Consumable, 
            "Health Potion", 
            new List<ItemEffect>
            {
                new HealEffect(EffectTrigger.OnUsed)
            },
            ItemID.HealthPotion
            ));
    }

    public ItemDescription GetDescription(ItemID iD)
    {
        return Database[iD];
    }

}
