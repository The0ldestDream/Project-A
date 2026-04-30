using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Agent agent;
    public AgentController agentController;
    public GridSystem grid; 
    
    private Vector2Int moveDirection;
    private Vector2 mosPos;
    private PlayerMode mode = PlayerMode.Exploration;


    //Refactor later
    public List<Door> doors = new List<Door>();



    public void Init(Agent player, GridSystem ourGrid, AgentController AC)
    {
        agent = player;
        grid = ourGrid;
        agentController = AC;
    }


    // Update is called once per frame
    void Update()
    {
 



    }


    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (mode != PlayerMode.Exploration) return;


        Vector2 readInput = context.ReadValue<Vector2>();
        moveDirection = Vector2Int.RoundToInt(readInput);
        


        Vector2Int DestinationCell = new Vector2Int(agent.gridPos.x, agent.gridPos.y);
        DestinationCell += moveDirection;

        //Debug.Log("The Player is at Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");
        //Debug.Log("The Player wants to move Destination Cell: (" + DestinationCell.x + ", " + DestinationCell.y + ")");

        DestinationCell.x = Mathf.Clamp(DestinationCell.x, 0, grid.width - 1);
        DestinationCell.y = Mathf.Clamp(DestinationCell.y, 0, grid.height - 1);

        GridCell dCell = grid.gridArray[DestinationCell.x, DestinationCell.y];
        
        if (IsValidMove(dCell))
        {
            agent.MoveTo(grid, dCell);
        }

        //Debug.Log("The Player has moved to Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");

    }

    public void Click(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        //if (mode != PlayerMode.Combat) return;

        Vector2 clickLocation = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(clickLocation.x, clickLocation.y));

        //Debug.Log(worldPosition);
        GridCell Destination = ConvertWorldToGridLocation(worldPosition);
        OnGridCellClicked?.Invoke(Destination);
    }

    public void Interact(InputAction.CallbackContext context) //Change this later into a interact with anything
    {
        if (!context.started) return;

        AgentAction interactAction = agent.allActions.Find(x => x.ActionName == "Interact");
        OnInteractPressed?.Invoke(agent, interactAction);
    }

    private void UseInteract(Target target)
    {
        AgentAction InteractAction = agent.allActions.Find(x => x.ActionName == "Interact");
        InteractAction.Action(agent, target, grid);
    }

    public void EndTurn(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (mode == PlayerMode.Combat)
        {
            if (agentController.state == CombatState.TurnInProgress)
            {
                agentController.EndTurn();
            }
            else
            {
                Debug.Log("Can't end turn because its another's agent turn");
            }
        }
        else
        {
            Debug.Log("Not in Combat");
        }
    }

    public void ChangeMode(PlayerMode newMode)
    {
        mode = newMode;
        Debug.Log("Player's mode has changed to " + mode);
    }

    private bool IsValidMove(GridCell DestinationCell)
    {
        if (DestinationCell.x < 0 || DestinationCell.x >= grid.width || DestinationCell.y < 0 || DestinationCell.y >= grid.height)
        {
            return false;
        }

        return true;
    }

    private GridCell ConvertWorldToGridLocation(Vector2 Location)
    {

        Vector2Int ClickLocation = Vector2Int.RoundToInt(Location);


        if(IsValidMove(grid.gridArray[ClickLocation.x, ClickLocation.y]))
        {
            GridCell newLocation = grid.gridArray[ClickLocation.x, ClickLocation.y];
            Debug.Log("This is grid cell at location: (" + newLocation.x + ", "+ newLocation.y + ")");
            return newLocation;
        }
        else
        {
            Debug.Log("Grid cell outside room bounds");
            return null;
        }

        
    }

    public static event Action<GridCell> OnGridCellClicked;
    public static event Action<Agent, AgentAction> OnInteractPressed;


    private void OnEnable()
    {
        ExplorationUI.OnInteractTargetChosen += UseInteract;
    }
    private void OnDisable()
    {
        ExplorationUI.OnInteractTargetChosen -= UseInteract;
    }
}
