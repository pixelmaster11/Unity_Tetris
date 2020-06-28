using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : Updateable
{   
    
    [SerializeField]
    private KeyboardInputConfig inputConfig;

   

    //Press down
    //Move command down.Execute() --> Triggers Move Down Event to which board is subscribed to 

    //Delegate with anonymous function
    //InputCommand moveDown = new MoveCommand(delegate (Board board) {board.FromInput("Move Down");}, "MoveDown");


    //Delegate with lambda expression
    //InputCommand moveDown = new MoveCommand( x => {x.FromInput("Move Down");}, "MoveDown");

    private InputCommand moveDir = new MoveCommand();
    private InputCommand rotateDir = new RotateCommand();
    private InputCommand snap = new SnapCommand();

    MoveEventArgs moveDownArgs = new MoveEventArgs(0, -1);
    MoveEventArgs moveLeftArgs = new MoveEventArgs(-1, 0);
    MoveEventArgs moveRightArgs = new MoveEventArgs(1, 0);
    RotateEventArgs rotateRightArgs = new RotateEventArgs(-1);
    RotateEventArgs rotateLeftArgs = new RotateEventArgs(1);

    Queue<InputCommand> inputCommands = new Queue<InputCommand>();

   


    private void GetInputs()
    {

        //Move Left
        if (Input.GetKeyDown(inputConfig.MoveLeft))
        {   
            moveDir.Execute(this.moveDir, moveLeftArgs);
                     
        }


        //Move Right
        if (Input.GetKeyDown(inputConfig.MoveRight))
        {
            moveDir.Execute(this.moveDir, moveRightArgs);

        }


        //Move Down
        if (Input.GetKeyDown(inputConfig.MoveDown))
        {
           
           moveDir.Execute(this.moveDir, moveDownArgs);

        }



         //Rotate Piece AC
        if (Input.GetKeyDown(inputConfig.RotateLeft))
        {

            rotateDir.Execute(this.rotateDir, rotateLeftArgs);

        }


        //Rotate Piece CC
        if (Input.GetKeyDown(inputConfig.RotateRight))
        {

            rotateDir.Execute(this.rotateDir, rotateRightArgs);

        }

        //Snap Piece
        if(Input.GetKeyDown(inputConfig.Snap))
        {   
            
           snap.Execute(this.snap, System.EventArgs.Empty);
        }


       
    }

    public override void Tick()
    {
        GetInputs();
    }
}
