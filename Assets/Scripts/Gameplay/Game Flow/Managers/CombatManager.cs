using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public Spawner AgentSpawner;
    public GameManager gameManager;


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

        Debug.Log("Spawned Enemy Units: ");
        AgentSpawner.SpawnAgent(room);



        Combat();
    }



    public void Combat()
    {
        //
        Debug.Log("Combat Has Started");
        //CombatSetup();


    }

    IEnumerator AgentInput()
    {

        yield return null;
    }

    //Events 
    public static event Action<Room> OnCombatStarted;
    public static void RaiseOnCombatStarted(Room room)
    {
        OnCombatStarted?.Invoke(room);
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
