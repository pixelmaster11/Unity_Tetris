using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveCommand : InputCommand
{   
  
    public override void Execute(object sender, EventArgs eventArgs)
    {   
       
        MoveEventArgs moveEventArgs = (MoveEventArgs) eventArgs;

        if(EventManager.MoveEvent != null)
        {
           EventManager.MoveEvent(moveEventArgs.xDir, moveEventArgs.yDir);
       
        }
    }
       
}
