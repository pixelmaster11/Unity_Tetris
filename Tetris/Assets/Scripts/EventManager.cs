﻿using System;

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
    public static Hold HoldCommandEvent;

    public delegate void HoldPiece(int id, int rotID);
    public static HoldPiece HoldPieceEvent;

    public delegate void TetrominoSpawn(Tetromino tetromino);
    public static TetrominoSpawn TetrominoSpawnEvent;

    public delegate void LineComplete(int linesCompleted);
    public static LineComplete LineCompleteEvent;


    public delegate void CommandSuccess();
    public static CommandSuccess MoveSuccessEvent;
    public static CommandSuccess RotateSuccessEvent;
    public static CommandSuccess SnapSuccessEvent;

    public delegate void FallTime(float fallTime);
    public static FallTime FallTimeDecreaseEvent;
}
