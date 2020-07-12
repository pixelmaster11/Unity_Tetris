using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// This state is responsible for checking line completion,
    ///  removing lines with chosen line clear method, and bring lines down after line clear
    /// </summary>
    public class LineCompletionBoardState : BoardState
    {

        //Keep track of lines complete
        private int linesCompleted;

        public LineCompletionBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
        {
            linesCompleted = 0;
        }

        public override void Entry()
        {
        
            CheckLineCompletion();
            stateController.ChangeState(BoardStateType.AutoFallState);
        }

        public override void Exit()
        {
            
        }

        public override void StateUpdate()
        {
            
        }


        /// <summary>
        /// Brings down lines at given row
        /// </summary>
        /// <param name="y"></param>
        public void DecreaseLine(int y) 
        {
            for (int x = 1; x < boardConfig.width - 1; x++) 
            {      
                // Move one towards bottom
                board.logicalBoard[x, y-1] = board.logicalBoard[x, y];
                board.graphicalBoard[x, y-1] = board.graphicalBoard[x , y];

                board.graphicalBoard[x, y] = null;

                // Update Sprite positions
                if(board.graphicalBoard[x , y - 1] != null)
                {
                    board.graphicalBoard[x, y-1].transform.position += new Vector3(0, - (1 + boardConfig.offY), 0);
                }
                
                
                
            }
        }


        public void DecreaseLinesAboveOf(int y) 
        {
            //For all lines above the completed line bring them down one by one
            for (int i = y; i < boardConfig.height; ++i)
                DecreaseLine(i);
        }


        /// <summary>
        /// Removes all sprites on the given row
        /// </summary>
        /// <param name="y">Row number</param>
        public void RemoveLineAt(int y) 
        {   
            //Remove all sprites for the given row except boundary sprites
            for (int x = 1; x < boardConfig.width - 1; x++) 
            {
                board.logicalBoard[x, y] = BoardConfig.EMPTY_SPACE;
                board.graphicalBoard[x,y].gameObject.SetActive(false);
                board.graphicalBoard[x, y] = null;
            }

            
        }

        /// <summary>
        /// Checks whether the line is completed at given row
        /// </summary>
        /// <param name="y">Row number</param>
        /// <returns>True of false if line is complete</returns>
        public bool IsLineCompleteAt(int y)
        {   
            //Ignore index 0 and width - 1 as they represent boundaries
            //Hence start loop from 1
            for (int x = 1; x < boardConfig.width - 1; x++)
            {
                //If there is any empty space, then line is not completed
                if (board.logicalBoard[x, y] == BoardConfig.EMPTY_SPACE)
                {
                    return false;
                }
                
            }
                
            return true;
        }

        //Line Completion Check
        public void CheckLineCompletion() 
        {
            // For all rows
            for (int y = 1; y < boardConfig.height; y++) 
            {   
                //Check if line is complete 
                if (IsLineCompleteAt(y)) 
                {   
                    
                    //Increment lines completed
                    linesCompleted += 1;

                    //Raise Line Complete Event
                    if(EventManager.LineCompleteEvent != null)
                    {
                        EventManager.LineCompleteEvent(linesCompleted);
                    }

                    //Remove completed line at that row
                    RemoveLineAt(y);

                    //Bring all rows from above the removed line down
                    DecreaseLinesAboveOf(y+1);

                    //Decrement the row as we have now brought all rows down due to line removal
                    y--;

                }
            }
                
            
        }
    }

}
