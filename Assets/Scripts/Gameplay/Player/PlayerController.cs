using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Agent agent;
    public GridSystem grid;
    private Vector2 moveDirection;
    public void Init(Agent player, GridSystem ourGrid)
    {
        agent = player;
        grid = ourGrid;
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

            if (IsValidMove(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }

        }

        if (moveDirection.y > 0)
        {
            DestinationCell.y += 1;
            if (IsValidMove(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.x < 0)
        {
            DestinationCell.x -= 1;
            if (IsValidMove(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.y < 0)
        {
            DestinationCell.y -= 1;
            if (IsValidMove(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }

        Debug.Log("The Player is at Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");
        Debug.Log("The Player wants to move Destination Cell: (" + DestinationCell.x + ", " + DestinationCell.y + ")");
    }

    public void ClickMove(InputAction.CallbackContext context)
    {

    }


    private bool IsValidMove(GridSystem grid, GridCell DestinationCell)
    {
        if (DestinationCell.x < 0 || DestinationCell.x >= grid.width || DestinationCell.y < 0 || DestinationCell.y >= grid.height || !grid.gridArray[DestinationCell.x, DestinationCell.y].walkable)
        {
            return false;
        }

        return DestinationCell.walkable;
    }

    private GridCell ConvertWorldToGridLocation(GridSystem grid, Vector2 ClickLocation)
    {
        GridCell newLocation = null;

        return newLocation;
    }


}
