using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    private GridCellHighlighter highlighter;
    public CombatUI combatUI;
    public ExplorationUI explorationUI;

    public PlayerMode UImode;

    bool ExplorationUICreated = false;
    bool CombatUICreated = false;
    

    public GameObject buttonPrefab;
    public Transform panel;
    public AgentController agenttouse;

    GameObject explorationUIObj;
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

                if (!ExplorationUICreated)
                {
                    InitExplorationUI();
                    ExplorationUICreated = true;
                }

                break;

            case PlayerMode.Combat:
                RemoveExplorationUI();

                if (!CombatUICreated)
                {
                    InitCombatUI();
                    CombatUICreated = true;

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
        explorationUIObj = new GameObject("CombatUI");
        explorationUI = explorationUIObj.AddComponent<ExplorationUI>();

        explorationUI.combatManager = gameManager.combatManager;
        explorationUI.highlighter = highlighter;

        explorationUI.InitUI();
    }
    private void RemoveExplorationUI()
    {
        if (explorationUIObj != null)
        {
            explorationUI.DestroySelf();
            ExplorationUICreated = false;
        }
    }

    private void RemoveCombatUI()
    {
        if (combatUIObj != null)
        {
            combatUI.DestroySelf();
            CombatUICreated = false;
        }
        
    }

    private void InitHighlighter()
    {
        GameObject CellHighlighter = new GameObject("GridCellHighlighter");
        highlighter = CellHighlighter.AddComponent<GridCellHighlighter>();

        highlighter.lGen = gameManager.levelManager.level.levelGenerator;
        highlighter.HightlightMap = gameManager.levelManager.level.levelRenderer.HighlightMap;
        highlighter.tileData = gameManager.levelManager.level.levelRenderer.highlighttileData;

        highlighter.InitHighlighter();
    }



    //Events
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }


}
