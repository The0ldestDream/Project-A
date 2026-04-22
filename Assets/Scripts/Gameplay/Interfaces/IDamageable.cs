using UnityEngine;

public interface IDamageable
{
    void DealDamage(int DamageTaken)
    {
        //AgentResource health = allResources.Find(x => x.ResourceName == "Health");
        //Debug.Log("Agent health is at: " + health.currentAmount);

        //health.AdjustValue(-DamageTaken);
        //Debug.Log("Agent health has dropped to: " + health.currentAmount);

        ////Could use a AgentDamage Event later on to tell listeners that this agent has been damaged


        //if (health.currentAmount <= 0)
        //{
        //    AgentDeath();
        //}
    }
}
