using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCommand : InputCommand
{
    public override void Execute(object sender, EventArgs eventArgs)
    {
        if(EventManager.SnapEvent != null)
        {
            EventManager.SnapEvent();
        }
    }
}
