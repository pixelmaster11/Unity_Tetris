﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.TetrominoSO
{   
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TetrominoConfig/Z")]
    public class TetrominoConfigZ : TetrominoConfig
    {
        private readonly int[,] MATRIX ={{ 0 , 1 , 0 , 0 },
                                        { 0 , 1 , 1 , 0 },
                                        { 0 , 0 , 1 , 0 },
                                        { 0 , 0 , 0 , 0 } };

        public override int[,] GetMatrix()
        {
            return MATRIX;
        }
    }
}
