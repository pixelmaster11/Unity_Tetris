using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    /// <summary>
    /// Issues Rotate Left / Right Command
    /// </summary>
    public class RotateCommand : InputCommand
    {
        public override void Execute(object sender, EventArgs eventArgs)
        {   
            //Get Rotate event arguemetns
            RotateEventArgs rotateEventArgs = (RotateEventArgs) eventArgs;

            //Raise Rotate Event
            if(EventManager.RotateEvent != null)
            {
                EventManager.RotateEvent(rotateEventArgs.rotateDirection);
            }
        }
    }

}
