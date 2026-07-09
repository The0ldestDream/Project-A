using UnityEngine;
using System;
using System.Collections.Generic;
public abstract class Item : IInteractable
{
    public string ItemName;

    public int Weight; // So the agent doesn't have unlimited space
    public int Value; // Could work how it does ins BG3

    public List<ItemEffect> itemEffects = new List<ItemEffect>();


    public void Interact(Agent agent)
    {
        agent.inventory.Add(this);
        OnPickedUp.Invoke();
    }

    


    //Events

    public event Action OnPickedUp;
    public event Action OnDropped;
}
