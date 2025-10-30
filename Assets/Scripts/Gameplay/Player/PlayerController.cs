using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Agent agent;
    AgentController agentController;
    public GridSystem grid; 
    
    private Vector2 moveDirection;
    private Vector2 mosPos;
    private PlayerMode mode = PlayerMode.Exploration;


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
        Vector2 readInput = context.ReadValue<Vector2>();
        moveDirection = readInput;//Vector2Int.RoundToInt(readInput);
        GridCell DestinationCell = agent.gridPos;

        Debug.Log("The Player is at Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");
        Debug.Log("The Player wants to move Destination Cell: (" + DestinationCell.x + ", " + DestinationCell.y + ")");
        if (moveDirection.x > 0)
        {
            DestinationCell.x += 1;

            if (IsValidMove(grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }

        }

        if (moveDirection.y > 0)
        {
            DestinationCell.y += 1;
            if (IsValidMove(grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.x < 0)
        {
            DestinationCell.x -= 1;
            if (IsValidMove(grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.y < 0)
        {
            DestinationCell.y -= 1;
            if (IsValidMove(grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }

        Debug.Log("The Player is at Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");
        Debug.Log("The Player wants to move Destination Cell: (" + DestinationCell.x + ", " + DestinationCell.y + ")");
    }

    public void ClickMove(InputAction.CallbackContext context)
    {

        if (mode == PlayerMode.Combat)
        {
            Vector2 clickLocation = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(clickLocation.x, clickLocation.y));

            Debug.Log(worldPosition);
            GridCell Destination = ConvertWorldToGridLocation(worldPosition);

            agentController.ClickMoveTo(grid, Destination);
        }

    }

    public void InteractDoor(InputAction.CallbackContext context)
    {
        Vector2 readInput = context.ReadValue<Vector2>();

        //get forward dir
        //check if door there
        //check if door is locked
        //if not open door



    }




    private bool IsValidMove(GridCell DestinationCell)
    {
        if (DestinationCell.x < 0 || DestinationCell.x >= grid.width || DestinationCell.y < 0 || DestinationCell.y >= grid.height || !grid.gridArray[DestinationCell.x, DestinationCell.y].walkable)
        {
            return false;
        }

        return DestinationCell.walkable;
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


}
