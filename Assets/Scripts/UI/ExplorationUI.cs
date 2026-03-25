using UnityEngine;
using System;
using System.Collections.Generic;

public class ExplorationUI : MonoBehaviour
{
    public CombatManager combatManager;
    public GridCellHighlighter highlighter;


    private List<GridCell> targetCells = new List<GridCell>();
    private List<Target> targetList = new List<Target>();

    public void InitUI()
    {
        combatManager.OnTargetingStarted += HandleGivenTargets;
        PlayerController.OnGridCellClicked += CheckIfClickedTileIsValid;

    }


    private void HandleGivenTargets(List<Target> targets)
    {
        //Highlight Given Targets

        highlighter.ClearTiles(targetCells);
        targetList.Clear();
        targetList = targets;
        targetCells.Clear();
        Debug.Log("RECIEVED INTERACTABLE OBJECTS");
        foreach (Target target in targets)
        {
            //Debug.Log(target);
            targetCells.Add(target.tile);
        }

        highlighter.HighlightTiles(targetCells);

    }


    public void CheckIfClickedTileIsValid(GridCell clickedcell)
    {
        Target TargetInList = targetList.Find(x => x.tile.Equals(clickedcell));

        if (TargetInList != null)
        {
            OnInteractTargetChosen?.Invoke(TargetInList);

            highlighter.ClearTiles(targetCells);
            targetCells.Clear();
            targetList.Clear();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public static event Action<Target> OnInteractTargetChosen;

    private void OnDisable()
    {
        combatManager.OnTargetingStarted -= HandleGivenTargets;
        PlayerController.OnGridCellClicked -= CheckIfClickedTileIsValid;
    }

}
