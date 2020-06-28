using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InputCommand : ICommand
{
    public abstract void Execute(object sender, EventArgs eventArgs);
    
  
}
