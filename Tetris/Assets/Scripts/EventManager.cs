using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Configs;
using Enums;

public class MoveEventArgs:EventArgs
{
    public readonly int xDir;
    public readonly int yDir;

    public MoveEventArgs(int _x, int _y)
    {
        xDir = _x;
        yDir = _y;
    }
}

public class RotateEventArgs : EventArgs
{
    public readonly int rotateDirection;

    public RotateEventArgs(int _r)
    {
        rotateDirection = _r;
    }
}



public static class EventManager
{
    
    public delegate void Move(int xDir, int yDir);
    public static Move MoveEvent;

    public delegate void Rotate(int rDir);
    public static Rotate RotateEvent;

    public delegate void Snap();
    public static Snap SnapEvent;





}
