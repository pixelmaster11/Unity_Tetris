using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using BoardSystem;
using InputSystem;
using Enums;

/// <summary>
/// Main Manager class.
/// Handles all other manager / controller classes.!--
/// Only this class inherits from Monobehaviour to have single Update / Start / Awake.!--
/// Controls other classes' updates / start 
/// </summary>
public class GamePlayManager : MonoBehaviour
{
    //Reference to configurables
    Board board;
    BoardStateController boardStateController;
    TetrominoManager tetrominoManager;
    InputManager inputManager;

    //Reference to Monobehaviour configurables
    [SerializeField]
    GameUIManager gameUIManager;
    [SerializeField]
    AudioManager audioManager;

    //Reference to Config file
    [SerializeField]
    private ConfigsReferences configsReferences;
    

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        EventManager.GameOverRestartEvent += Restart;
        Restart();
    }

    private void OnDisable()
    {
        EventManager.GameOverRestartEvent -= Restart;
    }


    void Initialize()
    {   

        //Create and set configurables
        //First all Monobehaviours

        if(gameUIManager == null)
        {
            gameUIManager = FindObjectOfType<GameUIManager>();        
        }
    
        if(audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();        
        }


        gameUIManager.SetSpawnConfig(configsReferences.GetConfig(ConfigType.TetrominoSpawn));
        audioManager.SetSpawnConfig(configsReferences.GetConfig(ConfigType.Audio));

        
        board = new Board(configsReferences.GetConfig(ConfigType.Board));
        tetrominoManager = new TetrominoManager(configsReferences.GetConfig(ConfigType.TetrominoSpawn));
        inputManager = new InputManager(configsReferences.GetConfig(ConfigType.KeyboardInput));
        boardStateController = new BoardStateController(board, tetrominoManager, BoardStateType.InitState);
        
      
    }


    void Update()
    {   
        inputManager.GetInputs();
        boardStateController.StateUpdate();
        gameUIManager.UIUpdate();
    }


    
    void Restart()
    {
        tetrominoManager.Enable();
        audioManager.Enable();
        boardStateController.Enable(BoardStateType.InitState);
    }

}
