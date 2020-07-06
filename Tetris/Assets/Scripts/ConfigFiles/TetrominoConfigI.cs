using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Configs.TetrominoSO
{

    /// <summary>
    /// Class to generate the I shape. Stores the 2d matrix for I shape which will be shared by as many I type tetromino instances
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TetrominoConfig/I", order = 1)]
    public class TetrominoConfigI : TetrominoConfig
    {
        private readonly int[,] MATRIX =    {{ 0 , 0 , 1 , 0 },
                                        { 0 , 0 , 1 , 0 },
                                        { 0 , 0 , 1 , 0 },
                                        { 0 , 0 , 1 , 0 } };


    /// <summary>
    /// Returns the I tetromino matrix
    /// </summary>
    /// <returns></returns>
        public override int[,] GetMatrix()
        {
            return MATRIX;
        }

      
    }
}
