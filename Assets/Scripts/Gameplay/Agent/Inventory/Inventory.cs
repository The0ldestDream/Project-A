using UnityEngine;
using System.Collections.Generic;
public class Inventory
{
    private List<ItemStack> items = new List<ItemStack>();


    public void Add(Item item)
    {
        ItemStack itemStack = Find(item);

        if (itemStack == null)
        {
            ItemStack stack = new ItemStack(item, 1);
            items.Add(stack);
        }
        else
        {
            itemStack.NumberOfItem++;
        }
    }

    public void Remove(Item item)
    {
        ItemStack itemStack = Find(item);

        if (itemStack.NumberOfItem > 1)
        {
            itemStack.NumberOfItem--;
        }
        else
        {
            itemStack.NumberOfItem--;
            items.Remove(itemStack);
        }

    }

    public ItemStack Find(Item item)
    {
        ItemStack itemStack = items.Find(x => x.item.ItemName == item.ItemName);

        return itemStack;
    }

    public bool ContainsItem(Item item)
    {
        ItemStack itemStack = Find(item);

        if (itemStack == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
