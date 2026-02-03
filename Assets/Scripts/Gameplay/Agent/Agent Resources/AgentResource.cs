using UnityEngine;

public abstract class AgentResource
{

    public string ResourceName;

    public int ResourceLevel;

    public int currentAmount;
    public int maxAmount;

    public AgentResource(string Name, int Level, int _maxAmount)
    {
        ResourceName = Name;
        ResourceLevel = Level;
        maxAmount = _maxAmount;
    }

    public abstract void OnLevelUp(int ClassLevel);

    //Used when a resource is used
    public void AdjustValue(int amount)
    {
        currentAmount += amount;
    }

    public void ResetToMax()
    {
        currentAmount = maxAmount;
    }

}
