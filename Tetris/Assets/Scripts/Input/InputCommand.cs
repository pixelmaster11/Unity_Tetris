using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace InputSystem
{

    /// <summary>
    /// Base class for all Input Commands
    /// </summary>
    public abstract class InputCommand : ICommand
    {
        public abstract void Execute(object sender, EventArgs eventArgs);
        
    }

}
