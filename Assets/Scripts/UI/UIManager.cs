using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;


    public CombatUI combatUI = new CombatUI();

    public PlayerMode UImode;

    bool UICreated = false;


    public GameObject buttonPrefab;
    public Transform panel;
    public AgentController agenttouse;

    
    public List<Button> TargetButtons = new List<Button>();

    private void Start()
    {

    }

    private void FixedUpdate()
    {

        agenttouse = gameManager.playerCharacter.agentController;


        

        UImode = gameManager.mode;

        switch (UImode)
        {
            case PlayerMode.Exploration:
                break;

            case PlayerMode.Combat:
                
                if (!UICreated)
                {
                    InitCombatUI();
                    UICreated = true;
                    if (!combatUI.buttonscreated)
                    {
                        combatUI.CreateActionButtons();
                    }    
                    
                }
                
                break;
        }

    }

    private void InitCombatUI()
    {
        GameObject combatUIObj = new GameObject("CombatUI");
        combatUI = combatUIObj.AddComponent<CombatUI>();
        combatUI.agenttouse = agenttouse;
        combatUI.buttonPrefab = buttonPrefab;
        combatUI.panel = panel;
    }

    private void InitExplorationUI()
    {

    }




    private void RemoveButtons(List<Button> buttons)
    {
        foreach (Button button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
    }






    //Events
    private void OnEnable()
    {
        //gameManager.combatManager.OnTargetingStarted += ShowTargetsOnGrid;
    }
    private void OnDisable()
    {
        //gameManager.combatManager.OnTargetingStarted -= ShowTargetsOnGrid;
    }


}
