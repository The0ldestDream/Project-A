using UnityEngine;

public class ItemStack
{
    public Item item;
    public int NumberOfItem;


    public ItemStack(Item newItem, int Number)
    {
        item = newItem;
        NumberOfItem = Number;
    }

}
