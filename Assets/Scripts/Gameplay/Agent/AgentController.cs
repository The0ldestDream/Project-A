using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AgentController : MonoBehaviour
{

    public Agent myAgent;

    public CombatState state;



    public void Init(Agent agent)
    {
        myAgent = agent;
    }

    void Update()
    {
        UpdateVisualPosition();
    }

    //Things to use in turns

    public void StartTurn()
    {
        state = CombatState.TurnInProgress;

        //This is where I need to add more inputs
        //Such as the capabilities to use actions and items etc
        //For now, the agent can just click move as much as they want as well as end their turn

        if (myAgent.alignment == AgentAlignment.Enemy)
        {
            Debug.Log("Enemy Agent Turn!");
            EndTurn();
        }

    }

    public void EndTurn()
    {
        Debug.Log("Agent has ended their turn!");
        state = CombatState.TurnCompleted;
        //Signal to Combat Manager that agent is done with their turn 
        OnTurnEnded?.Invoke(this);

    }


    public void UseAction(AgentAction action)
    {

    }




    //Movement
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




    //Visual Stuff
    public void UpdateVisualPosition()
    {

        if (myAgent != null)
        {
            transform.position = myAgent.gridPos.worldPos;
        }

    }


    //Events
    public event Action<AgentController> OnTurnEnded;

}
