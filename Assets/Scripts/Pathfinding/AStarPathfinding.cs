using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AStarPathfinding
{
    private GridCell start;
    private GridCell end;

    

    public List<GridCell> Pathfinding(GridCell startNode, GridCell endNode, GridSystem gridsystem)
    {
        start = startNode;
        end = endNode;

        GridCell[,] gridArray = gridsystem.gridArray;

        List<GridCell> openSet = new List<GridCell>(); //Nodes that are going to be evaluated
        openSet.Add(start);

        List<GridCell> closedSet = new List<GridCell>(); //Nodes already evaluated

        Dictionary<GridCell, GridCell> cameFrom = new Dictionary<GridCell, GridCell>();

        Dictionary<GridCell, int> gScore = new Dictionary<GridCell, int>();
        Dictionary<GridCell, int> fScore = new Dictionary<GridCell, int>();


        foreach (GridCell gridcell in gridArray)
        {
            gScore[gridcell] = int.MaxValue;
            fScore[gridcell] = int.MaxValue;
        }

        gScore[start] = 0;
        fScore[start] = manhattanDistance(start, end);

        while (openSet.Count > 0)
        {


            GridCell current = openSet.OrderBy(cell => fScore[cell]).First();
            if (current == end)
            {
                return CreateCorridorList(cameFrom);
            }

            closedSet.Add(current);
            openSet.Remove(current);
            foreach (GridCell neighbour in current.neighbours)
            {
                if (neighbour.walkable == false)
                {
                    continue;
                }

                if (!CheckIfCardinal(current,neighbour))
                {
                    continue;
                }

                if (closedSet.Contains(neighbour))
                {
                    continue;
                }


                int temp_gScore = gScore[current] + neighbour.cost;

                if (temp_gScore < gScore[neighbour])
                {

                    gScore[neighbour] = temp_gScore;
                    fScore[neighbour] = temp_gScore + manhattanDistance(neighbour, end);

                    cameFrom[neighbour] = current;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }

                }

            }


        }

        return null; // No Path Found
    }

    private List<GridCell> CreateCorridorList(Dictionary<GridCell, GridCell> cameFrom)
    {
        List<GridCell> cells = new List<GridCell>();

        GridCell currentNode = end;
        while (currentNode != start)
        {
            if (!CheckIfCardinal(currentNode, cameFrom[currentNode]))
            {
                Debug.LogWarning($"Diagonal in path! {currentNode.x},{currentNode.y} -> {cameFrom[currentNode].x},{cameFrom[currentNode].y}");
            }


            cells.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }

        cells.Add(start);

        return cells;
    }


    private int manhattanDistance(GridCell a, GridCell b)
    {

        int distance = Mathf.Abs((a.x - b.x) + (a.y - b.y));

        return distance;
    }


    private bool CheckIfCardinal(GridCell a, GridCell b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);

        if (dx + dy == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
