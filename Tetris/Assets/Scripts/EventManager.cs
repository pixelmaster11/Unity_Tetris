using System;

/// <summary>
/// Custom Movement event arguement class.!--
/// Used to pass custom arguements to commands
/// </summary>
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

/// <summary>
/// Custom Rotate event arguement class
/// Used to pass custom arguements to commands
/// </summary>
public class RotateEventArgs : EventArgs
{
    public readonly int rotateDirection;

    public RotateEventArgs(int _r)
    {
        rotateDirection = _r;
    }
}


/// <summary>
/// Main class responsible for all events
/// </summary>
public static class EventManager
{
    //Move left / right event
    public delegate void Move(int xDir, int yDir);
    public static Move MoveEvent;

    //Rotate left / right event
    public delegate void Rotate(int rDir);
    public static Rotate RotateEvent;

    //Snap event
    public delegate void Snap();
    public static Snap SnapEvent;

    public delegate void Hold();
    public static Hold HoldEvent;

    public delegate void TetrominoSpawn(int id);
    public static TetrominoSpawn TetrominoSpawnEvent;
}
