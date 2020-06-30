using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.TetrominoSO
{
   /// <summary>
   /// Class to generate the T shape. Stores the 2d matrix for T shape which will be shared by as many T type tetromino instances
   /// </summary>
   [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TetrominoConfig/T")]
   public class TetrominoConfigT:TetrominoConfig
   {
      private readonly int[,] MATRIX ={{ 0 , 0 , 0 , 0 },
                                       { 0 , 0 , 1 , 0 },
                                       { 0 , 1 , 1 , 0 },
                                       { 0 , 0 , 1 , 0 } };


      /// <summary>
      /// Returns the T type 2d matrix
      /// </summary>
      /// <returns></returns>
      public override int[,] GetMatrix()
      {
         return MATRIX;
      }
   }
}


