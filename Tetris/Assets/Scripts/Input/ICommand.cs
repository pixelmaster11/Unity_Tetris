using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base interface for commands
/// </summary>
public interface ICommand 
{
    //Contains sender object and event arguements
    void Execute(object sender, EventArgs eventArgs);
}
