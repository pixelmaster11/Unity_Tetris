using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Configs
{
   /// <summary>
   /// Responsible for all tetromino spawn settings
   /// </summary>
   [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TetrominoSpawnConfig", order = 1)]
   public class TetrominoSpawnConfig : BaseConfig
   {  
      //list of all tetromino prefabs
      public List<Tetromino> TetrominoPrefabs;

      //list of all tetromino prefabs
      public List<PreviewTetromino> PreviewTetrominoPrefabs;

      //Amount of tetrominos and its sprites to pool
      public int TetrominoPoolAmount;
      public int TetrominoSpritePoolAmount;

      //Type of method used to spawn tetrominos
      public TetrominoSpawnType TetrominoSpawnType; 
     
      //Maximum tetrominos in queue to display in ui
      public int MaxTetrominosInQueue = 3;

      
   }

}