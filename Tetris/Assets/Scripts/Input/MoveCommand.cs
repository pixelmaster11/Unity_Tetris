using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InputSystem
{

    /// <summary>
    /// Issues a Move left / right command
    /// </summary>
    public class MoveCommand : InputCommand
    {   
    
        public override void Execute(object sender, EventArgs eventArgs)
        {   
            //Get movement arguements
            MoveEventArgs moveEventArgs = (MoveEventArgs) eventArgs;
            
            //Raise Move Event
            if(EventManager.MoveEvent != null)
            {
                EventManager.MoveEvent(moveEventArgs.xDir, moveEventArgs.yDir);       
            }
        }
        
    }

}
