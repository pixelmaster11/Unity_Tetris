using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICommand 
{
    void Execute(object sender, EventArgs eventArgs);
}
