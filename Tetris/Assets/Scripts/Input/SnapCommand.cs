using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    /// <summary>
    /// Issues a Snap command
    /// </summary>
    public class SnapCommand : InputCommand
    {
        public override void Execute(object sender, EventArgs eventArgs)
        {   
            //Raise Snap Event
            if(EventManager.SnapEvent != null)
            {
                EventManager.SnapEvent();
            }
        }
    }

}
