using UnityEngine;
using System.Collections.Generic;
public class TargetingSystem
{    
    public List<Target> ReturnValidTargets(GridSystem grid, Agent ActionOwner, AgentAction action)
    {
        TargetCategory category = action.target;
        int range = action.Range;
        int NumberOfTargets = action.TargetCount;
        List<Target> TargetsWithinRange = FindTargetsWithinRange(grid, ActionOwner, range);
        List<Target> validTargets = new List<Target>();


        switch (category)
        {
            case TargetCategory.Self:
                Target selfTarget = new Target(grid.gridArray[ActionOwner.gridPos.x, ActionOwner.gridPos.y]);
                validTargets.Add(selfTarget);

                break;
            case TargetCategory.Agent:
           
                foreach (Target target in TargetsWithinRange)
                {
                    
                    if (target.agent != null)
                    {
                        validTargets.Add(target);

                    }
                }
                break;

            case TargetCategory.Tile:
              
                foreach (Target target in TargetsWithinRange)
                {
                    validTargets.Add(target);
                }
                break;
        }
        return validTargets;
    }

    public List<Target> FindTargetsWithinRange(GridSystem grid, Agent ActionOwner, int ActionRange)
    {
        List<Target> targets = new List<Target>();

        GridCell OriginPosition = grid.gridArray[ActionOwner.gridPos.x, ActionOwner.gridPos.y];

        //Creating a box around the Origin Position
        //Then we can loop through each cell and choose the ones we want
        //Probably should create some criteria for which cells that are not valid but I'll do that later
        for (int x = OriginPosition.x - ActionRange; x <= OriginPosition.x + ActionRange; x++)
        {
            for (int y = OriginPosition.y - ActionRange; y <= OriginPosition.y + ActionRange; y++)
            {
                if (x >= 0 && y >= 0 && x < grid.gridArray.GetLength(0) && y < grid.gridArray.GetLength(1)) //Bounds Check
                {
                    if (grid.gridArray[x, y].TypeOfTile == TileType.roomTile) // Need to only work within the current room. Currently uses any room tile even other rooms
                    {
                        Target target = new Target(grid.gridArray[x, y]);
                        targets.Add(target);
                    }
                    
                }
            }
        }
        return targets;
    }
}
