using UnityEngine;
using System.Collections.Generic;
public class AIController
{
    private Agent myAgent;

    private TargetingSystem targeting = new TargetingSystem();
    private ShapeHelper helper = new ShapeHelper();

    public void Init(Agent agent)
    {
        myAgent = agent;
    }

    public void DetermineAction()
    {
        List<AgentAction> usableableActions = new List<AgentAction>();



        foreach (AgentAction action in myAgent.allActions)
        {
            FindBestAction(action);
        }
    }

    private void FindBestTile()
    {

    }

    private void FindBestAction(AgentAction action)
    {
        switch (action.target)
        {
            case TargetCategory.Self:
                break;

            case TargetCategory.Agent:
                //targeting.ReturnValidTargets(grid, myAgent, action);
                break;

            case TargetCategory.Tile:
                break;
        
        }


    }


}
