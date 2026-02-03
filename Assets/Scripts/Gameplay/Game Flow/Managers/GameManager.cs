using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public PlayerMode mode;
    public PlayerController playerCharacter = null;

    public LevelSetup level;
    public CombatManager combatManager;
    public UIManager uiManager;

    private void OnEnable()
    {
        OnPlayerInteracted += HandleDoorInteraction;
    }

    private void OnDisable()
    {
        OnPlayerInteracted -= HandleDoorInteraction;

    }


    void Start()
    {
        ChangeMode(PlayerMode.Exploration, playerCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCharacter == null && level.levelGenerated == true) 
        {
            playerCharacter = level.spawnedPlayer.GetComponent<PlayerController>();

            playerCharacter.doors = level.levelGenerator.tGen.allDoors;
        }
    }



    public void ChangeMode(PlayerMode newMode, PlayerController player)
    {
        mode = newMode;
        player.ChangeMode(newMode);
        Debug.Log("Mode has changed to " + mode);
    }

    public void HandleTranistionToCombat(Door door)
    {
        ChangeMode(PlayerMode.Transition, playerCharacter);

        List<GridCell> MoveToCells = door.DoorOwner.GetClosestCellsFrom(door, 6, level.levelGenerator.ourGrid);
        List<Door> RoomDoors = door.DoorOwner.roomDoors;

        int RandCell = UnityEngine.Random.Range(0, MoveToCells.Count);

        playerCharacter.agentController.ClickMoveTo(level.levelGenerator.ourGrid, MoveToCells[RandCell]);

        ChangeMode(PlayerMode.Combat, playerCharacter);

        foreach (Door currentDoor in RoomDoors)
        {
            currentDoor.LockDoor();
            currentDoor.doorOpen = false;
        }

        CombatManager.RaiseOnCombatStarted(door.DoorOwner);
    }

    private IEnumerator TriggerTheStateChange(Door door)
    {
        yield return null;
        HandleTranistionToCombat(door);
    }

    public void HandleDoorInteraction(Door door)
    {
        if (door.DoorOwner.roomState == RoomState.Uncleared)
        {
            StartCoroutine(TriggerTheStateChange(door));

            
        }
    }

    //Events
    public static event Action<Door> OnPlayerInteracted;

    public static void RaisePlayerInteracted(Door door)
    {
        OnPlayerInteracted?.Invoke(door);
    }


}
