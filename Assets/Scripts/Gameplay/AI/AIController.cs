using UnityEngine;
using System.Collections.Generic;
public class AIController
{
    private Agent myAgent;

    private TargetingSystem targeting = new TargetingSystem();
    private ShapeHelper helper = new ShapeHelper();

    public AIController(Agent agent)
    {
        myAgent = agent;
    }

    public void DetermineAction(GridSystem grid, List<AgentController> agents)
    {
        int bestscore = 0;
        AgentAction bestAction = null;
        Target bestTarget = null;

        // Go through each action the agent has 
        // Add the actions that can affect the player's team
        foreach (AgentAction action in myAgent.allActions)
        {
            List<Target> targets = targeting.ReturnValidTargets(grid, myAgent, action);
            

            foreach (Target target in targets)
            {
                List<GridCell> cells = helper.GetShape(grid, myAgent, action, target);
                int score = 0;

                foreach (GridCell cell in cells)
                {
                    if (cell.AgentOnTile != null && cell.AgentOnTile.controller.AIC != this)
                    {
                        score++;
                    }

                }

                if (score > bestscore)
                {
                    bestscore = score;
                    bestAction = action;
                    bestTarget = target;
                }

            }
        }

        // Choose the action that hits the most targets
        if (bestAction != null)
        {
            bestAction.Action(myAgent, bestTarget, grid);
        }
        else
        {
            AgentAction move = myAgent.allActions.Find(x => x.ActionName == "Move");
            move.Action(myAgent, MoveTowardEnemy(grid, move, agents), grid);
        }
    }

 

    private Target MoveTowardEnemy(GridSystem grid, AgentAction action, List<AgentController> agents)
    {
        List<Target> targets = targeting.ReturnValidTargets(grid, myAgent, action);

        int closestManhattenDistance = 10000;
        Target closestTarget = null;

        foreach (Target target in targets)
        {
            foreach (AgentController agent in agents)
            {
                if (agent.AIC == this || agent.myAgent.alignment == AgentAlignment.Enemy)
                {
                    continue;
                }

                int dx = Mathf.Abs(target.tile.x - agent.myAgent.gridPos.x);
                int dy = Mathf.Abs(target.tile.y - agent.myAgent.gridPos.y);

                int manhatten = dx + dy;

                if (manhatten < closestManhattenDistance)
                {
                    closestManhattenDistance = manhatten;
                    closestTarget = target;
                }
            }
        }

        return closestTarget;
    }


}
