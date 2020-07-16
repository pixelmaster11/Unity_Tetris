using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;
namespace BoardSystem
{
    public class GameOverBoardState : BoardState
    {   
        private TetrominoManager tetrominoManager;

        public GameOverBoardState(Board _board, BoardStateController _controller, TetrominoManager _tMan) : base(_board, _controller)
        {
            tetrominoManager = _tMan;
        }

        public override void Entry()
        {
            tetrominoManager.OnGameOver();
            
            for(int y = 0; y < boardConfig.height; y++)
            {
                for (int x = 0; x < boardConfig.width; x++)
                {
                    //If boundary
                    if(y == 0 || x == 0 || x == (boardConfig.width - 1))
                    {
                        
                        board.graphicalBoard[x, y] = null;
                        board.logicalBoard[x, y] = BoardConfig.BOUNDARY;
                    } 

                    //BG tile
                    else
                    {
                        board.graphicalBoard[x, y] = null;
                        board.logicalBoard[x, y] = BoardConfig.EMPTY_SPACE;
                    }
                                            
                }
            }

            //Raise game over event
            if(EventManager.GameOverEvent != null)
            {
                EventManager.GameOverEvent();
            }

            //DebugUtils.Log(this, "GameOver");
        }

     
        public override void StateUpdate()
        {
            
        }


        public override void Exit()
        {
           
        }
    }

}
