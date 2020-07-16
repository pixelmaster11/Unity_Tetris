using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class GameStateController : MonoBehaviour
{
        //Current and previous states
        private GameState currentState;
        private GameState previousState;

        //Current and previous state types
        private GameStateType currentStateType;
        private GameStateType previousStateType;

        //Dictionary to store all states
        private Dictionary<GameStateType, GameState> gameStates;
        private float timer = 0;

        [SerializeField]
        private GameState menuState;

        [SerializeField]
        private GameState gameplayState;

        private void Awake()
        {
            Init();
        }

        //On Create
        public void Init()
        {   
            //Fill up the dictionary by creating and adding all game states
            gameStates = new Dictionary<GameStateType, GameState>();
            gameStates.Add(GameStateType.MenuState, menuState);
            gameStates.Add(GameStateType.GamePlayState, gameplayState);

            InitializeGameState();
        }

        /// <summary>
        /// Assigns a start state to the board
        /// </summary>
        /// <param name="initStateType">The start state of the board</param>
        public void InitializeGameState()
        {
           
            if(menuState != null)
            {
                currentState = menuState;
            }
    
                
            LogState("Entry");
            currentState.Entry();
                

        }

        /// <summary>
        /// Change from current state to new state
        /// </summary>
        /// <param name="newStateType">New state to transition to</param>
        public void ChangeState(GameStateType newStateType)
        {
            GameState newState;

            if(currentStateType == newStateType)
            {
                return;
            }

            if(gameStates.TryGetValue(newStateType, out newState))
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

        /// <summary>
        /// Logs current state status
        /// </summary>
        /// <param name="message"></param>
        private void LogState(string message)
        {
            DebugUtils.LogState(currentState, message);
        }

}

