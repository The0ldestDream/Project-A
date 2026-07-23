using UnityEngine;
using System;
using System.Collections.Generic;
public abstract class Item : IInteractable
{
    public string ItemName;

    public int Weight; // So the agent doesn't have unlimited space
    public int Value; // Could work how it does ins BG3


    public bool Reusable = false;

    public List<ItemEffect> itemEffects = new List<ItemEffect>();

    public Item()
    {

    }


    public abstract bool Activate(Agent agent);


    public void Interact(Agent agent)
    {
        agent.inventory.Add(this);
        OnPickedUp.Invoke();
    }


    //Events

    public event Action OnPickedUp;
    public event Action OnDropped;
}
