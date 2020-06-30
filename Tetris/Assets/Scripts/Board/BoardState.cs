using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// Base class for all Board States
    /// </summary>
    public abstract class BoardState : IState
    {
        //Reference to board state machine 
        protected BoardStateController stateController;

        //Reference to Board data
        protected Board board;

        //Reference to cache board config file
        protected BoardConfig boardConfig;

    
        public abstract void Entry();
        public abstract void StateUpdate();
        public abstract void Exit();

        //Cache References 
        public BoardState(Board _board, BoardStateController _controller)
        {
            board = _board;
            stateController = _controller;
            boardConfig = board.boardConfig;

        }

    }
    
}
