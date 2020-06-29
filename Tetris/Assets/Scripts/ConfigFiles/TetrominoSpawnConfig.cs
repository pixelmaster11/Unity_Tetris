using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Configs
{
   
   [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TetrominoSpawnConfig", order = 1)]
   public class TetrominoSpawnConfig : BaseConfig
   {
      public List<Tetromino> TetrominoPrefabs;
      public int TetrominoPoolAmount;
      public int TetrominoSpritePoolAmount;
      public TetrominoSpawnType TetrominoSpawnType; 
      //public int[] TetrominoSpawnQueue;

   }

}