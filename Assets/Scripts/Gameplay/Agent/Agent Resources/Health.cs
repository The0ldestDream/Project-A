using UnityEngine;

public class Health : AgentResource
{
    
    public Health() : base("Health", 1, 10)
    {

    }

    public override void OnLevelUp(int ClassLevel)
    {
        throw new System.NotImplementedException();
    }
}
