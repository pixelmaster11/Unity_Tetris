using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

public class BoardStateController 
{
    
    private BoardState currentState;
    private BoardState previousState;

    private BoardStateType currentStateType;
    private BoardStateType previousStateType;

    private Dictionary<BoardStateType, BoardState> boardStates;
    private float timer = 0;
   
    public BoardStateController(Board board, TetrominoManager tetrominoManager, BoardStateType initStateType)
    {   
        
        boardStates = new Dictionary<BoardStateType, BoardState>();
        boardStates.Add(BoardStateType.InitState, new InitBoardState(board, this));
        boardStates.Add(BoardStateType.AutoFallState, new AutoFallBoardState(board, this, tetrominoManager)); 
        boardStates.Add(BoardStateType.LockingState, new LockingBoardState(board, this));
        boardStates.Add(BoardStateType.LineCompletionState, new LineCompletionBoardState(board, this));               

        InitializeBoardState(initStateType);
    }


    public void InitializeBoardState(BoardStateType initStateType)
    {
        BoardState initState;

        if(boardStates.TryGetValue(initStateType, out initState))
        {
            currentState = initState;
            
            LogState("Entry");
            currentState.Entry();
            
        }

        else
        {
            Debug.LogError(initStateType + " is not a valid board state type");
        }
       
    }

    public void ChangeState(BoardStateType newStateType)
    {
        BoardState newState;
        if(currentStateType == newStateType)
        {
            return;
        }

        if(boardStates.TryGetValue(newStateType, out newState))
        {
            //check null for initialization
            if(currentState != null)
            {   
                LogState("Exit");

                //Exit the current state
                currentState.Exit();               
                
            }

           
            //Make current state as previous
            previousState = currentState;
            previousStateType = currentStateType;

            //Make the new state as current
            currentState = newState;
            currentStateType = newStateType;

            LogState("Entry");

            //Enter the new state
            currentState.Entry();

            
        }

        else
        {
            DebugUtils.LogError(this, newStateType + " is not a valid board state type");
        }
        

       
    }



    /// <summary>
    /// Conitnuously update the the state i.e keep the current state contionuously running
    /// </summary>
    public void StateUpdate()
    {
        if(currentState != null)
        {
            //Keep running the current state
            currentState.StateUpdate();
            //_LogState("Update: " + Time.time);
        }
    }


    /// <summary>
    /// Similar to State Update except this will run every not per frame but after xx seconds
    /// </summary>
    /// <param name="timeDelay"> Time after every second this function will run </param>
    public void StateUpdateDelayed(float timeDelay)
    {
        

        if (currentState != null)
        {
            //Run after every xx seconds
            if(timer >= timeDelay)
            {
                currentState.StateUpdate();
                timer = 0;
            }

            else
            {
                timer += Time.deltaTime;
            }
              
        }
    }

    private void LogState(string message)
    {
        DebugUtils.LogState(currentState, message);
    }

 


}
