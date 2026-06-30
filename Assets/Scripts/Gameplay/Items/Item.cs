using UnityEngine;

public abstract class Item
{
    public string ItemName;

    public int Weight; // So the agent doesn't have unlimited space
    public int Value; // Could work how it does ins BG3

    public abstract void UseItem();
}
