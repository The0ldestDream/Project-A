using UnityEngine;
using System;
using System.Collections.Generic;
public abstract class Item : IInteractable
{
    public string ItemName;
    public ItemID ID;

    public int Weight; // So the agent doesn't have unlimited space
    public int Value; // Could work how it does ins BG3
    public bool Reusable = false;

    public GridCell gridPos;

    public List<ItemEffect> itemEffects = new List<ItemEffect>();

    public Item()
    {

    }

    public void SetTile(GridCell cell, bool OnTile)
    {
        if (OnTile)
        {
            cell.EntityOnTile = EntityType.Item;
            cell.interactable = this;
            cell.walkable = false;
        }
        else if (!OnTile)
        {
            cell.EntityOnTile = EntityType.None;
            cell.interactable = null;
            cell.walkable = true;
        }

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
