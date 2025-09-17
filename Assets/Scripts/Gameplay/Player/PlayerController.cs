using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Agent agent;
    public GridSystem grid;
    private Vector2Int moveDirection;
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
        moveDirection = Vector2Int.RoundToInt(readInput);
        GridCell DestinationCell = agent.gridPos;
        

        //Debug.Log(moveDirection);
        if (moveDirection.x > 0)
        {
            DestinationCell.x += 1;

            if (IsValidMove(grid, DestinationCell))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }

            
        }

        if (moveDirection.y > 0)
        {
            DestinationCell.y += 1;
            if (IsValidMove(grid, DestinationCell))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.x < 0)
        {
            DestinationCell.x -= 1;
            if (IsValidMove(grid, DestinationCell))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }
        if (moveDirection.y < 0)
        {
            DestinationCell.y -= 1;
            if (IsValidMove(grid, DestinationCell))
            {
                agent.MoveTo(grid, grid.gridArray[DestinationCell.x, DestinationCell.y]);
            }
                
        }

        Debug.Log("The Player is at Grid Cell: (" + agent.gridPos.x + ", " + agent.gridPos.y + ")");
    }

    private bool IsValidMove(GridSystem grid, GridCell DestinationCell)
    {
        if (DestinationCell.x < 0 || DestinationCell.x >= grid.width || DestinationCell.y < 0 || DestinationCell.y >= grid.height)
        {
            return false;
        }

        return DestinationCell.walkable;
    }

}
