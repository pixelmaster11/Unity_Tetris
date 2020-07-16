using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace BoardSystem
{

    /// <summary>
    /// This state is responsible for locking a tetromino piece on board
    /// </summary>
    public class LockingBoardState : BoardState
    {
        
        

        public LockingBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
        {
            
        }

        public override void Entry()
        {     
            LockPiece();  
            stateController.ChangeState(BoardStateType.LineCompletionState);
            
        }

        public override void Exit()
        {
            LockComplete();
        }

        public override void StateUpdate()
        {
        
        }

        //Lock Piece on Board
        private void LockPiece()
        {
        
            //TUT : y -> Y-axis, x -> X-axis
            for (int x = 0; x < board.currPiece.GetLength(1); x++)
            {
                for (int y = 0; y < board.currPiece.GetLength(0); y++)
                {
                    //Piece position on board
                    int boardX = x + board.currentPosX;
                    int boardY = y + board.currentPosY;

                    //Check if inside boundaries, otherwise don't do anything
                    //wE SIMPLY DO NOT CARE
                    if (boardX >= 0 && boardX < boardConfig.width)
                    {
                        if (boardY >= 0 && boardY < boardConfig.height)
                        {
                            //If its a tetromino position from tetromino matrix
                            if (board.currPiece[y, x] == 1)
                            { 
                                board.logicalBoard[boardX, boardY] = board.tetromino.GetTetrominoID(); 

                                //After locking the tetromino, its sprites becomes part of the board
                                if(board.graphicalBoard[boardX, boardY] != null)
                                {
                                    board.graphicalBoard[boardX, boardY].transform.parent = board.boardTetrominos;
                                    
                                }  
                                                

                            }
                        }
                    }

                }
            }

          

        }


        private void LockComplete()
        {   
            
            //Raise Snap Success event
            if(EventManager.SnapSuccessEvent != null)
            {
                EventManager.SnapSuccessEvent();
            }

        }
    
    }

}
