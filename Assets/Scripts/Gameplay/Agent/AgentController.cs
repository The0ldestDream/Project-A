using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentController : MonoBehaviour
{

    public Agent myAgent;
    
    public void Init(Agent agent)
    {
        myAgent = agent;
    }

    void Update()
    {
        UpdateVisualPosition();
    }


    public void ClickMoveTo(GridSystem grid, GridCell Destination)
    {
        List<GridCell> path = myAgent.pathfinding.Pathfinding(myAgent.gridPos, Destination, grid);

        StartCoroutine(MoveAlongPath(grid, path));


    }


    IEnumerator MoveAlongPath(GridSystem grid, List<GridCell> path)
    {
        foreach (GridCell cell in path)
        {
            myAgent.MoveTo(grid, cell);

            yield return new WaitForSeconds(0.5f);
        }

    }

    public void UpdateVisualPosition()
    {

        if (myAgent != null)
        {
            transform.position = myAgent.gridPos.worldPos;
        }

    }
}
