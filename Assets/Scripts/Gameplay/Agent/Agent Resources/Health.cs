using UnityEngine;

public class Health : AgentResource
{
    
    public Health(StatSheet AgentStats) : base("Health", 1, 10)
    {
        DetermineAgentHealth(AgentStats);
    }

    public void DetermineAgentHealth(StatSheet AgentStats)
    {
        int consValue = AgentStats.GetStatValue("Consititution");
        int modifier = Mathf.RoundToInt((float)(consValue * 0.2));
        maxAmount = 10 + modifier;
    }

    public override void OnLevelUp(int ClassLevel)
    {
        throw new System.NotImplementedException();
    }
}
