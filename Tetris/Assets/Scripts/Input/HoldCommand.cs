using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{

    /// <summary>
    /// Issues a hold piece command
    /// </summary>
    public class HoldCommand : InputCommand
    {
        public override void Execute(object sender, EventArgs eventArgs)
        {
             //Raise Hold Event
            if(EventManager.HoldCommandEvent != null)
            {
                EventManager.HoldCommandEvent();       
            }
        }
    }
}
