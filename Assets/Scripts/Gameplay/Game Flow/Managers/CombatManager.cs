using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    public Spawner AgentSpawner;
    public GameManager gameManager;

    private bool combatFinished;
    private List<AgentController> AgentsInCombat = new List<AgentController>();

    Queue<AgentController> TurnOrder = new Queue<AgentController>();

    public AgentController currentAgent;

    void Start()
    {
        
    }
    void Update()
    {
        
    }


    public void CombatSetup(Room room)
    {
        //Spawn Combat Grid over room
        //Spawn Enemy Agents
        AgentSpawner.SpawnAgent(room);
        // Get all agents involved in combat
        AgentsInCombat = GetAgentsInCombat(room);
        Debug.Log("Spawned Enemy Units: ");
        foreach (AgentController agent in AgentsInCombat)
        {
            Debug.Log(agent);
        }
        TurnOrder = DetermineTurnOrder(AgentsInCombat);
        currentAgent = TurnOrder.Peek();
        StartNextTurn();
    }



    public void StartNextTurn()
    {
        
        //Tell them to do their turn
        currentAgent.StartTurn();

    }

    public void HandleTurnEnded(AgentController agent)
    {
        if (currentAgent != agent)
        {
            
        }
        TurnOrder.Enqueue(TurnOrder.Dequeue()); // Takes the item that has been removed and places it at the end like a rotation. Might need a way to track the number of turns.
        
        EndOfTurnEvents(currentAgent);

        if (CheckIfCombatHasEnded())
        {
            //We need to declare that it has ended and clear everything
            //Transition game states back to exploration
            
            foreach (AgentController ac in TurnOrder)
            {
                UnsubscribeAgent(ac);
            }
            RaiseOnCombatEnded();
        }
        else
        {
            //If combat is not done, then continue here
            //Want to set the new current agent
            currentAgent = TurnOrder.Peek();
            StartNextTurn();
        }
    }



    public void EndOfTurnEvents(AgentController agent)
    {
        //Handles any end of turn effects, check if agent is dead, end of combat conditions
    }


    public bool CheckIfCombatHasEnded()
    {
        int NumberOfFriendlies = 0;
        int NumberOfEnemies = 0;

        foreach (AgentController agent in TurnOrder)
        {
            if (agent.myAgent.alignment == AgentAlignment.Friendly)
            {
                NumberOfFriendlies += 1;
            }
            if (agent.myAgent.alignment == AgentAlignment.Enemy)
            {
                NumberOfEnemies += 1;
            }    
        }

        // If the number of friendly or enemy agents is equal to 0
        // No agent on the opposite side to fight which means combat is over
        if (NumberOfFriendlies == 0 || NumberOfEnemies == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    public Queue<AgentController> DetermineTurnOrder(List<AgentController> AgentsInCombat)
    {
        Queue<AgentController> AgentTurnOrder = new Queue<AgentController>();

        foreach (AgentController agent in AgentsInCombat)
        {
            AgentTurnOrder.Enqueue(agent);
            
        }

        AgentTurnOrder.Enqueue(AgentTurnOrder.Dequeue()); // getting the player to the front

        return AgentTurnOrder;
    }

    public List<AgentController> GetAgentsInCombat(Room room) //Probably will need to change to check cells instead
    {
        List<AgentController> AgentsInSpace = new List<AgentController>();

        foreach (GameObject AgentObject in AgentSpawner.EnemyAgents)
        {
            
            AgentController ac = AgentObject.GetComponent<AgentController>();
            SubscribeAgent(ac);


            AgentsInSpace.Add(ac);
        }

        AgentsInSpace.Add(gameManager.playerCharacter.agentController);
        SubscribeAgent(gameManager.playerCharacter.agentController);
        return AgentsInSpace;
    }

    private void SubscribeAgent(AgentController ac)
    {
        ac.OnTurnEnded -= HandleTurnEnded;
        ac.OnTurnEnded += HandleTurnEnded;
    }
    private void UnsubscribeAgent(AgentController ac)
    {
        ac.OnTurnEnded -= HandleTurnEnded;
    }


    //Events 
    public event Action<Room> OnCombatStarted;
    public event Action OnCombatEnded;
    public void RaiseOnCombatStarted(Room room)
    {
        OnCombatStarted?.Invoke(room);
    }
    public void RaiseOnCombatEnded()
    {
        OnCombatEnded?.Invoke();
    }


    public void OnEnable()
    {
        OnCombatStarted += CombatSetup;
    }

    public void OnDisable()
    {
        OnCombatStarted -= CombatSetup;
    }

}
