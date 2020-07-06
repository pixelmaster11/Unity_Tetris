using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Common Rotation Method using matrix rotation
/// This method can be used to rotate any custom made tetromino piece 
/// </summary>
public class MatrixRotate : IRotateStrategy
{
    //Rotate by 90 deg in CC and AC
    public int[,] Rotate (int[,] piece, int direction, Tetromino T)
    {
      
        int dim = piece.GetLength(0);
       
        //TUT: EXPLAIN ROTATION
        int[,] npiece = new int[dim, dim];


        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                if (direction == -1)
                {
                   npiece[i, j] = piece[dim - 1 - j, i];
              
                }
                   
                else
                {
                    npiece[i, j] = piece[j, dim - 1 - i];  
               
                }
                    
            }

        }


        return npiece;
    }


 
}
