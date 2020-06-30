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
/// Controls other class updates / start 
/// </summary>
public class GamePlayManager : MonoBehaviour
{
    //Reference to configurables
    Board board;
    TetrominoManager tetrominoManager;
    InputManager inputManager;
    BoardStateController boardStateController;

    //Reference to Config file
    [SerializeField]
    private ConfigsReferences configsReferences;

 

    private void Start()
    {
        Initialize();
    }


    void Initialize()
    {   
        //Create and set configurables
        board = new Board(configsReferences.GetConfig(ConfigType.Board));
        tetrominoManager = new TetrominoManager(configsReferences.GetConfig(ConfigType.TetrominoSpawn));
        inputManager = new InputManager(configsReferences.GetConfig(ConfigType.KeyboardInput));

        boardStateController = new BoardStateController(board, tetrominoManager, BoardStateType.InitState);
        
    }


    void Update()
    {   
        inputManager.GetInputs();
        boardStateController.StateUpdate();
    }

}
