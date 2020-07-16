using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// A Naive Line clear implementation 
    /// </summary>
    public class NaiveLineClear : ILineClearStrategy
    {   
        //References
        Board board;
        BoardConfig boardConfig;

        //Keep track of lines complete
        private int linesCompleted;

        //Init and cache references
        public NaiveLineClear(Board _board)
        {
            linesCompleted = 0;
            board = _board;
            boardConfig = board.boardConfig;
        }

        public void ClearLine()
        {
            CheckLineCompletion();
        }


                /// <summary>
        /// Brings down lines at given row
        /// </summary>
        /// <param name="y"></param>
        private void DecreaseLine(int y) 
        {
            for (int x = 1; x < boardConfig.width - 1; x++) 
            {      
                // Move one line down
                board.logicalBoard[x, y-1] = board.logicalBoard[x, y];
                board.graphicalBoard[x, y-1] = board.graphicalBoard[x , y];

                //Disable previous sprites
                board.graphicalBoard[x, y] = null;

                // Update Sprite positions
                if(board.graphicalBoard[x , y - 1] != null)
                {
                    board.graphicalBoard[x, y-1].transform.position += new Vector3(0, - (1 + boardConfig.offY), 0);
                }
                
                
                
            }
        }


        private void DecreaseLinesAboveOf(int y) 
        {
            //For all lines above the completed line bring them down one by one
            for (int i = y; i < boardConfig.height; ++i)
                DecreaseLine(i);
        }


        /// <summary>
        /// Removes all sprites on the given row
        /// </summary>
        /// <param name="y">Row number</param>
        private void RemoveLineAt(int y) 
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
        private bool IsLineCompleteAt(int y)
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
        private void CheckLineCompletion() 
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

                    //Bring all rows down from above the removed line 
                    DecreaseLinesAboveOf(y+1);

                    //Decrement the row as we have now brought all rows down due to line removal
                    y--;

                }
            }
                
            
        }
    }

}
