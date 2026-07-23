using UnityEngine;
using System.Collections.Generic;
public class ItemDescription
{
    public ItemType type;
    public string ItemName;
    public List<ItemEffect> itemEffects = new List<ItemEffect>();
    public float Weight;
    public bool Reusable;
    public ItemID ID;

    public ItemDescription(ItemType itemType, string Name, List<ItemEffect> effects, ItemID itemID)
    {
        type = itemType;
        ItemName = Name;
        itemEffects = effects;
        ID = itemID;    
    }

}
