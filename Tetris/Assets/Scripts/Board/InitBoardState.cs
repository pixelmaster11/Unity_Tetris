using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

public class InitBoardState : BoardState
{   
    
    public InitBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
    {
        
    }

    public override void Entry()
    {         
        InitializeBoard();       
        stateController.ChangeState(BoardStateType.AutoFallState);
    }

    public override void Exit()
    {
      
    }

    public override void StateUpdate()
    {
       
    }


     //Init State
    private void InitializeBoard()
    {
        board.logicalBoard = new int[boardConfig.width, boardConfig.height];
        board.graphicalBoard = new SpriteRenderer[boardConfig.width, boardConfig.height];

        for(int y = 0; y < boardConfig.height; y++)
        {
            for (int x = 0; x < boardConfig.width; x++)
            {
                //If boundary
                if(y == 0 || x == 0 || x == (boardConfig.width - 1))
                {
                    Object.Instantiate(boardConfig.boundarySprite, 
                    new Vector2(x + x * boardConfig.offX, y + y * boardConfig.offY), 
                    Quaternion.identity).transform.parent = board.boardSprites;

                    board.graphicalBoard[x, y] = null;
                    board.logicalBoard[x, y] = BoardConfig.BOUNDARY;
                } 

                //BG tile
                else
                {
                    Object.Instantiate(boardConfig.backgroundSprite, 
                    new Vector2(x + x * boardConfig.offX, y + y * boardConfig.offY), 
                    Quaternion.identity).transform.parent = board.boardSprites;

                    board.graphicalBoard[x, y] = null;
                    board.logicalBoard[x, y] = BoardConfig.EMPTY_SPACE;
                }
                                           
            }
        }

    
       
 
    }

    
}
