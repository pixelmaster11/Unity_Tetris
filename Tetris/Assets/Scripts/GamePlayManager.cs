using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Enums;

public class GamePlayManager : MonoBehaviour
{
   
    Board board;
    TetrominoManager tetrominoManager;
    InputManager inputManager;
    BoardStateController boardStateController;

    [SerializeField]
    private ConfigsReferences configsReferences;

 

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
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
