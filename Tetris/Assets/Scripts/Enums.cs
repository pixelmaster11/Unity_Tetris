using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File to store all enum typess
/// </summary>
namespace Enums
{
    public enum TetrominoType
    {
        I = 0,
        T,
        O,
        S,
        Z,
        J,
        L
    }

    public enum TetrominoSpawnType
    {
        Random = 0,
        Queued
    }

    public enum BoardStateType
    {
        InitState = 0,
        AutoFallState,
        LockingState,
        LineCompletionState,
        GameOverState
    }


    public enum ConfigType
    {
        Board = 0,
        KeyboardInput,
        TetrominoSpawn,
        Tetromino,
        Audio
    }

    public enum RotateType
    {
        Matrix = 0,
        Nintendo,
        Gameboy,
        Original,
        Sega,
        Arika,
        Super
    }

    public enum HoldType
    {
        Unlimited = 0,
        Once
    }

}
