using UnityEngine;
using System;
public abstract class Item : IInteractable
{
    public string ItemName;

    public int Weight; // So the agent doesn't have unlimited space
    public int Value; // Could work how it does ins BG3

    public void Interact(Agent agent)
    {
        agent.inventory.Add(this);
        OnPickedUp.Invoke();
    }

    public abstract void UseItem();
    


    //Events

    public event Action OnPickedUp;
    public event Action OnDropped;
}
