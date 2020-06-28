using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : InputCommand
{
    public override void Execute(object sender, EventArgs eventArgs)
    {
        RotateEventArgs rotateEventArgs = (RotateEventArgs) eventArgs;

        if(EventManager.RotateEvent != null)
        {
            EventManager.RotateEvent(rotateEventArgs.rotateDirection);
        }
    }
}
