using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CombatUI : MonoBehaviour
{
    public List<Button> ActionButtons = new List<Button>();
    private Vector2 buttonOffset = new Vector2(0, -100);

    public AgentController agenttouse;
    public GameObject buttonPrefab;
    public Transform panel;
    public bool buttonscreated = false;
    
    
    
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

        action.Action(agenttouse.myAgent, new Target(null));


    }
}
