using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface is used to apply a chosen rotation algorithm
/// </summary>
public interface IRotateStrategy 
{
    int[,] Rotate(int[,] piece, int rotateDir, Tetromino T);
}
