using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

public abstract class BoardState : IState
{
    protected BoardStateController stateController;
    protected Board board;
    protected BoardConfig boardConfig;

   
    public abstract void Entry();
    public abstract void StateUpdate();
    public abstract void Exit();


    public BoardState(Board _board, BoardStateController _controller)
    {
        board = _board;
        stateController = _controller;
        boardConfig = board.boardConfig;

    }


 
  
}
