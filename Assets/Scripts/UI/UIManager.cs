using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject buttonPrefab;
    public Transform panel;
    public AgentController agenttouse;

    bool buttonscreated = false;
    private Vector2 buttonOffset = new Vector2(0, -100);
    public List<Button> ActionButtons = new List<Button>();
    public List<Button> TargetButtons = new List<Button>();

    private void FixedUpdate()
    {
        agenttouse = gameManager.playerCharacter.agentController;
        if (agenttouse != null)
        {
            if (buttonscreated == false)
            {
                CreateActionButtons();
            }
        }
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

            //button.GetComponentInChildren<Text>().text = action.ActionName;
            
            //Getting the actual button and assigning it to the Action's execute function
            Button buttonParent = button.GetComponent<Button>();
            buttonParent.onClick.AddListener(() => UseAgentAction(action));

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


    private void UseAgentAction(AgentAction action)
    {

        //action.Action(agenttouse.myAgent);
       

    }

    private void RemoveButtons(List<Button> buttons)
    {
        foreach (Button button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
    }


    public void ShowTargetsOnGrid(List<Target> targets)
    {
        RemoveButtons(TargetButtons);
        int counter = 0;
        foreach (Target target in targets)
        {
            counter += 1;
            //Instatiating the button and placing it under the panel
            //Then assigning the text of the button to the name of the action
            GameObject button = Instantiate(buttonPrefab, panel);
            TextMeshProUGUI buttontext = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttontext != null)
            {
                buttontext.text = counter.ToString();
            }
            else
            {
                Debug.Log("TMP Text is not found");
            }

            //button.GetComponentInChildren<Text>().text = action.ActionName;

            //Getting the actual button and assigning it to the Action's execute function
            Button buttonParent = button.GetComponent<Button>();
            buttonParent.onClick.AddListener(() => OnTargetSelected(target));

            ActionButtons.Add(buttonParent);
        }
    }


    public void OnTargetSelected(Target target)
    {

    }
    public void HighlightTile()
    {

    }



    //Events
    private void OnEnable()
    {
        gameManager.combatManager.OnTargetingStarted += ShowTargetsOnGrid;
    }
    private void OnDisable()
    {
        gameManager.combatManager.OnTargetingStarted -= ShowTargetsOnGrid;
    }


}
