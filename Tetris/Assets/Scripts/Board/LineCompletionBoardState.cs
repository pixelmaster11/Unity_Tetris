using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

public class LineCompletionBoardState : BoardState
{

    public LineCompletionBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
    {
     
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


    
    public void DecreaseLine(int y) 
    {
        for (int x = 1; x < boardConfig.width - 1; x++) 
        {
            
            // Move one towards bottom
            board.logicalBoard[x, y-1] = board.logicalBoard[x, y];
            board.graphicalBoard[x, y-1] = board.graphicalBoard[x , y];

            board.graphicalBoard[x, y] = null;

            // Update Block position
            if(board.graphicalBoard[x , y - 1] != null)
            {
                board.graphicalBoard[x, y-1].transform.position += new Vector3(0, - (1 + boardConfig.offY), 0);
            }
            
            
            
        }
    }


    public void DecreaseLinesAboveOf(int y) 
    {
        for (int i = y; i < boardConfig.height; ++i)
            DecreaseLine(i);
    }


    public void RemoveLineAt(int y) 
    {
        for (int x = 1; x < boardConfig.width - 1; x++) 
        {
            board.logicalBoard[x, y] = BoardConfig.EMPTY_SPACE;
            board.graphicalBoard[x,y].gameObject.SetActive(false);
            board.graphicalBoard[x, y] = null;
        }

        
    }

    public bool IsLineCompleteAt(int y)
    {
        for (int x = 1; x < boardConfig.width - 1; x++)
        {
            if (board.logicalBoard[x, y] == BoardConfig.EMPTY_SPACE)
            {
                 return false;
            }
               
        }
            
        return true;
    }

    //Line Completion State
    public void CheckLineCompletion() 
    {
        
        for (int y = 1; y < boardConfig.height; y++) 
        {
            if (IsLineCompleteAt(y)) 
            {   
                
                RemoveLineAt(y);
                DecreaseLinesAboveOf(y+1);
                y--;
            }
        }
             
        
    }
}
