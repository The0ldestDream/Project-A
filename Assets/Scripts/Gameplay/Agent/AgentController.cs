using UnityEngine;

public class AgentController : MonoBehaviour
{

    public Agent myAgent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Agent agent)
    {
        myAgent = agent;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisualPosition();
    }

    public void UpdateVisualPosition()
    {

        if (myAgent != null)
        {
            transform.position = myAgent.gridPos.worldPos;
        }

    }
}
