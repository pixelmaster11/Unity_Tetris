using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

namespace InputSystem
{

    /// <summary>
    /// Responsible to fetch Input and issue input commands.!-- Inherits from base configurable
    /// </summary>
    public class InputManager : Configurable
    {   
        
        //Press down
        //Move command down.Execute() --> Triggers Move Down Event to which board is subscribed to 

        //Delegate with anonymous function
        //InputCommand moveDown = new MoveCommand(delegate (Board board) {board.FromInput("Move Down");}, "MoveDown");


        //Delegate with lambda expression
        //InputCommand moveDown = new MoveCommand( x => {x.FromInput("Move Down");}, "MoveDown");

        //Reference to input config file
        private KeyboardInputConfig inputConfig;

        //Input commands
        private InputCommand moveDir = new MoveCommand();
        private InputCommand rotateDir = new RotateCommand();
        private InputCommand snap = new SnapCommand();
        private InputCommand hold = new HoldCommand();

        //Left / Right move and rotate event arguements
        MoveEventArgs moveDownArgs = new MoveEventArgs(0, -1);
        MoveEventArgs moveLeftArgs = new MoveEventArgs(-1, 0);
        MoveEventArgs moveRightArgs = new MoveEventArgs(1, 0);
        RotateEventArgs rotateRightArgs = new RotateEventArgs(1);
        RotateEventArgs rotateLeftArgs = new RotateEventArgs(-1);

        //Stack to store input commands
        Stack<InputCommand> inputCommands = new Stack<InputCommand>();

        //Accept and set config file 
        public InputManager(BaseConfig _config) : base (_config)
        {
            inputConfig = (KeyboardInputConfig) _config;
        }
        
        /// <summary>
        /// Get inputs
        /// </summary>
        public void GetInputs()
        {

            //Move Left
            if (Input.GetKeyDown(inputConfig.MoveLeft))
            {   
                moveDir.Execute(this.moveDir, moveLeftArgs);
                inputCommands.Push(moveDir);         
            }


            //Move Right
            if (Input.GetKeyDown(inputConfig.MoveRight))
            {
                moveDir.Execute(this.moveDir, moveRightArgs);
                inputCommands.Push(moveDir); 
            }


            //Move Down
            if (Input.GetKeyDown(inputConfig.MoveDown))
            {       
                moveDir.Execute(this.moveDir, moveDownArgs);
                inputCommands.Push(moveDir); 
            }



            //Rotate Piece AC
            if (Input.GetKeyDown(inputConfig.RotateLeft))
            {
                rotateDir.Execute(this.rotateDir, rotateLeftArgs);
                inputCommands.Push(rotateDir); 
            }


            //Rotate Piece CC
            if (Input.GetKeyDown(inputConfig.RotateRight))
            {
                rotateDir.Execute(this.rotateDir, rotateRightArgs);
                inputCommands.Push(rotateDir); 
            }

            //Snap Piece
            if(Input.GetKeyDown(inputConfig.Snap))
            {             
                snap.Execute(this.snap, System.EventArgs.Empty);
                inputCommands.Push(snap); 
            }


             //Hold Piece
            if(Input.GetKeyDown(inputConfig.Hold))
            {             
                hold.Execute(this.hold, System.EventArgs.Empty);
                inputCommands.Push(hold); 
            }

        
        }

    
    }

}
