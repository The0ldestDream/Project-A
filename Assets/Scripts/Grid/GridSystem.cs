using UnityEngine;
using System.Collections.Generic;

public class GridSystem
{
    public int width;
    public int height;

    public Vector2 gridOrigin = new Vector2(0, 0);

    public GridCell[,] gridArray;

    public void GenerateGrid(int gridWidth, int gridHeight)
    {
        width = gridWidth;
        height = gridHeight;

        gridArray = new GridCell[width, height];

        for (int x= 0; x < width; x++)
        {
            for (int y=0; y < height; y++)
            {
                gridArray[x, y] = new GridCell(x, y);
            }
        }
        setNeighbours();
        setWorldPositions();
    }

    private void setNeighbours()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //sets neighbours var in cell with getNeighbours
                gridArray[x,y].neighbours = getNeighbours(gridArray[x, y]);
            }
        }
    }

    private void setWorldPositions()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Setting World Positions
                // Grid Origin is (0,0)
                // I want to offset the cells accordingly

                gridArray[x, y].worldPos = new Vector2(
                    (gridOrigin.x) + x * gridArray[x, y].cellwidth + gridArray[x, y].cellwidth / 2f,
                    (gridOrigin.y) + y * gridArray[x, y].cellheight + gridArray[x, y].cellheight / 2f
                    );
            }
        }
    }

    private List<GridCell> getNeighbours(GridCell centerCell)
    {
        List<GridCell> cellNeighbours = new List<GridCell>();

        //Sides
        //Left Neighbour - > (x-1, y)
        if (centerCell.x > 0)
        {
            cellNeighbours.Add(gridArray[centerCell.x - 1, centerCell.y]);
        }
        //Right Neighbour - > (x+1,y)
        if (centerCell.x < width - 1)
        {
            cellNeighbours.Add(gridArray[centerCell.x + 1, centerCell.y]);
        }
        //Down Neighbour - > (x, y-1)
        if (centerCell.y > 0)
        {
            cellNeighbours.Add(gridArray[centerCell.x, centerCell.y - 1]);
        }
        //Up Neighbour - > (x, y+1)
        if (centerCell.y < height - 1)
        {
            cellNeighbours.Add(gridArray[centerCell.x, centerCell.y + 1]);
        }
        //Diagonals
        // Top Right - > (x+1, y+1)
        if (centerCell.y < height - 1 && centerCell.x < width - 1)
        {
            cellNeighbours.Add(gridArray[centerCell.x + 1, centerCell.y + 1]);
        }
        // Top Left - > (x-1, y+1)
        if (centerCell.y < height - 1 && centerCell.x > 0)
        {
            cellNeighbours.Add(gridArray[centerCell.x - 1, centerCell.y + 1]);
        }
        // Bot Right - > (x+1, y-1)
        if (centerCell.x < width - 1 && centerCell.y > 0)
        {
            cellNeighbours.Add(gridArray[centerCell.x + 1, centerCell.y - 1]);
        }
        // Bot Left - > (x-1,y-1)
        if (centerCell.y > 0 && centerCell.x > 0)
        {
            cellNeighbours.Add(gridArray[centerCell.x - 1, centerCell.y - 1]);
        }


        return cellNeighbours;
    }


}
