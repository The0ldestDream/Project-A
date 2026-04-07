using UnityEngine;
using System.Collections.Generic;
public class ShapeHelper
{
    
    public List<GridCell> FindCellsWithinRadius(GridSystem grid, GridCell CenterPoint, int Radius)
    {
        List<GridCell> CellsInRadius = new List<GridCell>();

        List<GridCell> CellsWithinSquare = new List<GridCell>();

        GridCell OriginPosition = grid.gridArray[CenterPoint.x, CenterPoint.y];

        //Creating a box around the Origin Position
        //Then we can loop through each cell and choose the ones we want
        for (int x = OriginPosition.x - Radius; x <= OriginPosition.x + Radius; x++)
        {
            for (int y = OriginPosition.y - Radius; y <= OriginPosition.y + Radius; y++)
            {
                if (x >= 0 && y >= 0 && x < grid.gridArray.GetLength(0) && y < grid.gridArray.GetLength(1)) //Bounds Check
                {
                    if (grid.gridArray[x, y].TypeOfTile == TileType.roomTile || grid.gridArray[x, y].TypeOfTile == TileType.doorTile) // Need to only work within the current room. Currently uses any room tile even other rooms
                    {

                        CellsWithinSquare.Add(grid.gridArray[x, y]);
                    }

                }
            }
        }

        foreach (GridCell cell in CellsWithinSquare)
        {
            int dx = Mathf.Abs(CenterPoint.x - cell.x);
            int dy = Mathf.Abs(CenterPoint.y - cell.y);

            int manhattan = dx + dy;

            if (manhattan < Radius)
            {
                CellsInRadius.Add(cell);
            }
        }

        return CellsInRadius;
    }

    public List<GridCell> FindCellsWithinLine(GridSystem grid, GridCell Start, GridCell End)
    {
        List<GridCell> CellsInLine = new List<GridCell>();

        GridCell currentCell = Start;
        int error = 0;

        int dx = End.x - Start.x;
        int dy = End.y - Start.y;

        var Direction = DetermineDirection(Start, End);

        int directionX = Direction.x;
        int directionY = Direction.y;


        // X Dominant 
        if (Mathf.Abs(dx) >= Mathf.Abs(dy))
        {
            while (currentCell.x != End.x)
            {
                currentCell = grid.gridArray[currentCell.x + directionX, currentCell.y];
                
                error += Mathf.Abs(dy);

                if (error >= Mathf.Abs(dx))
                {
                    currentCell = grid.gridArray[currentCell.x, currentCell.y + directionY];
                    error -= Mathf.Abs(dx);
                }

                CellsInLine.Add(grid.gridArray[currentCell.x, currentCell.y]);
            }
        }
        else // Y Dominant
        {
            while (currentCell.y != End.y)
            {
                currentCell = grid.gridArray[currentCell.x, currentCell.y + directionY];

                error += Mathf.Abs(dx);

                if (error >= Mathf.Abs(dy))
                {
                    currentCell = grid.gridArray[currentCell.x + directionX, currentCell.y];
                    error -= Mathf.Abs(dy);
                }

                CellsInLine.Add(grid.gridArray[currentCell.x, currentCell.y]);
            }
        }

        return CellsInLine;

    }

    public List<GridCell> FindCellsWithinCone(GridSystem grid, GridCell Start, GridCell End)
    {
        List<GridCell> CellsInCone = new List<GridCell>();

        int dx = End.x - Start.x;
        int dy = End.y - Start.y;
        // Direction = (x,y)

        var Direction = DetermineDirection(Start, End);
        int directionX = Direction.x;
        int directionY = Direction.y;


        return CellsInCone;
    }

    public GridCell NearestUnoccupiedCell(GridSystem grid, GridCell Start, GridCell End)
    {
        List<GridCell> unoccupiedCells = new List<GridCell>();

        foreach (GridCell neighbour in End.neighbours)
        {
            if (neighbour.interactable == null && neighbour.EntityOnTile == EntityType.None)
            {
                unoccupiedCells.Add(neighbour);
            }
        }

        int closestManhattenDistance = Mathf.FloorToInt(Mathf.Infinity);
        GridCell closestCell = null;

        foreach (GridCell cell in unoccupiedCells)
        {
            int dx = Mathf.Abs(Start.x - End.x);
            int dy = Mathf.Abs(Start.y - End.y);

            int manhatten = dx + dy;

            if (manhatten < closestManhattenDistance)
            {
                closestManhattenDistance = manhatten;
                closestCell = cell;
            }
        }


        return closestCell;
    }


    private (int x, int y) DetermineDirection(GridCell Start, GridCell End)
    {
        int directionX = 0;
        int directionY = 0;
        int dx = End.x - Start.x;
        int dy = End.y - Start.y;

        if (dx > 0)
        {
            directionX = 1;
        }
        else if (dx < 0)
        {
            directionX = -1;
        }
        else
        {
            directionX = 0;
        }

        if (dy > 0)
        {
            directionY = 1;
        }
        else if (dy < 0)
        {
            directionY = -1;
        }
        else
        {
            directionY = 0;
        }

        return (directionX, directionY);
    }

}
