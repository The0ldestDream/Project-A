using UnityEngine;
using System.Collections.Generic;
public class ItemDatabase : MonoBehaviour
{
    private Dictionary<ItemID, ItemDescription> Database = new Dictionary<ItemID, ItemDescription>();


    public void Init()
    {
        //Add the Items here
    }



    public ItemDescription GetDescription(ItemID iD)
    {
        return Database[iD];
    }


    public void CreateItem(ItemID iD)
    {
        switch (iD)
        {
            case ItemID.HealthPotion:

                break;
        
        }

    }

}
