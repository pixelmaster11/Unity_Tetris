using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace BoardSystem
{
    /// <summary>
    /// Initialization Board state.
    /// Responsible to initialize the board on play
    /// Can include different methods to initialize different types of levels
    /// </summary>
    public class InitBoardState : BoardState
    {   
        private bool initComplete;

        public InitBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
        {
            
        }

        public override void Entry()
        {   
            if(!initComplete)
            {
                InitializeBoard();     
            }     
              
            stateController.ChangeState(BoardStateType.AutoFallState);
        }

        public override void Exit()
        {
            initComplete = true;
        }

        public override void StateUpdate()
        {
        
        }


        //Init
        private void InitializeBoard()
        {
            //Create board and spawn sprites
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

}
