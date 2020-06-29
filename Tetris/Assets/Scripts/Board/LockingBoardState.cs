using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

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
         
    }

    public override void StateUpdate()
    {
       
    }

     //Locking State
    public void LockPiece()
    {
       
   
        //TUT : y -> Y-axis, x -> X-axis
        for (int x = 0; x < board.currPiece.GetLength(1); x++)
        {
            for (int y = 0; y < board.currPiece.GetLength(0); y++)
            {
                //Piece position on board
                //TUT: EXPLAIN THIS
                int boardX = x + board.currentPosX;
                int boardY = y + board.currentPosY;

                //Check if inside boundaries, otherwise don't do anything
                //TUT: EXPLAIN WHAT HAPPENS IF OUTSIDE BOUNDARY
                //wE SIMPLY DO NOT CARE
                if (boardX >= 0 && boardX < boardConfig.width)
                {
                    if (boardY >= 0 && boardY < boardConfig.height)
                    {
                        //If its a tetromino position from tetromino matrix
                        if (board.currPiece[y, x] == 1)
                        {
                            //Lock the piece logically                          
                           //isLocked = true;
                          
                           board.logicalBoard[boardX, boardY] = board.tetromino.GetTetrominoID(); 

                            //After locking the tetromino, its sprites becomes part of the board
                           if(board.graphicalBoard[boardX, boardY] != null)  
                                board.graphicalBoard[boardX, boardY].transform.parent = board.boardTetrominos;         

                        }
                    }
                }

            }
        }


        //CheckLineCompletion();
        //DisableTetromino();
        //GetTetromino(); 
    }
}
