using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    private GridCellHighlighter highlighter;
    public CombatUI combatUI;


    public PlayerMode UImode;

    bool UICreated = false;
    

    public GameObject buttonPrefab;
    public Transform panel;
    public AgentController agenttouse;

    GameObject combatUIObj;


    public List<Button> TargetButtons = new List<Button>();

    private void Start()
    {
        InitHighlighter();
    }

    private void FixedUpdate()
    {

        agenttouse = gameManager.playerCharacter.agentController;


        

        UImode = gameManager.mode;

        switch (UImode)
        {
            case PlayerMode.Exploration:
                RemoveCombatUI();
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
        combatUIObj = new GameObject("CombatUI");
        combatUI = combatUIObj.AddComponent<CombatUI>();

        combatUI.combatManager = gameManager.combatManager;
        combatUI.highlighter = highlighter;

        combatUI.agenttouse = agenttouse;
        combatUI.buttonPrefab = buttonPrefab;
        combatUI.panel = panel;

        combatUI.InitUI();
    }

    private void InitExplorationUI()
    {

    }

    private void RemoveCombatUI()
    {
        if (combatUIObj != null)
        {
            combatUI.DestroySelf();
            UICreated = false;
        }
        
    }

    private void InitHighlighter()
    {
        GameObject CellHighlighter = new GameObject("GridCellHighlighter");
        highlighter = CellHighlighter.AddComponent<GridCellHighlighter>();

        highlighter.lGen = gameManager.level.levelGenerator;
        highlighter.HightlightMap = gameManager.level.levelRenderer.HighlightMap;
        highlighter.tileData = gameManager.level.levelRenderer.highlighttileData;

        highlighter.InitHighlighter();
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
