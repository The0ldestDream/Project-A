using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CombatUI : MonoBehaviour
{
    public CombatManager combatManager;

    
    public List<Button> ActionButtons = new List<Button>();
    private Vector2 buttonOffset = new Vector2(0, -100);

    public AgentController agenttouse;
    public GameObject buttonPrefab;
    public Transform panel;

    public GridCellHighlighter highlighter;

    public AgentAction selectedAction;
    List<GridCell> targetCells = new List<GridCell>();
    List<Target> targetList = new List<Target>();

    public bool buttonscreated = false;

    public void InitUI()
    {
        combatManager.OnTargetingStarted += HandleGivenTargets;
        PlayerController.OnGridCellClicked += CheckIfClickedTileIsValid;
    }

    public void CreateActionButtons()
    {
        //Essientally getting all the actions that the agent has and creating buttons for each one
        foreach (AgentAction action in agenttouse.myAgent.allActions)
        {
            //Instatiating the button and placing it under the panel
            //Then assigning the text of the button to the name of the action
            GameObject button = Instantiate(buttonPrefab, panel);
            TextMeshProUGUI buttontext = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttontext != null)
            {
                buttontext.text = action.ActionName;
            }
            else
            {
                Debug.Log("TMP Text is not found");
            }

            //Getting the actual button and assigning it to the Action's execute function
            Button buttonParent = button.GetComponent<Button>();
            buttonParent.onClick.AddListener(() => SelectedAction(action));

            ActionButtons.Add(buttonParent);
        }

        buttonscreated = true;

        int index = 0;
        foreach (Button button1 in ActionButtons)
        {
            RectTransform rectTransform = button1.GetComponent<RectTransform>();

            Vector2 newPosition = buttonOffset * (index + 1);
            rectTransform.anchoredPosition = newPosition;


            index++;

        }
    }


    private void SelectedAction(AgentAction action)
    {
        if (selectedAction != action)
        {
            highlighter.ClearTiles(targetCells);
        }

        combatManager.StartTargeting(agenttouse.myAgent, action);
        selectedAction = action;

    }

    private void HandleGivenTargets(List<Target> targets)
    {
        //Highlight Given Targets
        //I need to figure out to highlight given tiles
        //Was thinking to maybe make something that interacts with renderer
        //Or implementing another tilemap ontop of the current to highlight the borders of the tiles
        targetList.Clear();
        targetList = targets;

        Debug.Log("RECIEVED TARGETS");
        foreach (Target target in targets)
        {
            Debug.Log(target);
            targetCells.Add(target.tile);
        }

        highlighter.HighlightTiles(targetCells);

    }


    private void UseAgentAction(AgentAction action, Target target)
    {
        action.Action(agenttouse.myAgent, target);
    }

    public void CheckIfClickedTileIsValid(GridCell clickedcell)
    {
        Target TargetInList = targetList.Find(x => x.tile.Equals(clickedcell));

        if (TargetInList != null)
        {
            UseAgentAction(selectedAction, TargetInList);
        }
    }




    //Events
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        combatManager.OnTargetingStarted -= HandleGivenTargets;
        PlayerController.OnGridCellClicked -= CheckIfClickedTileIsValid;
    }
}
