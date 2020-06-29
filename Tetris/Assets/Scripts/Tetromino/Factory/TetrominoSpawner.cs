using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace TetrominoSpawnSystem
{

    public class TetrominoSpawner 
    {
        //Reference to Tetromino Factory and Sprite Factory
        TetrominoFactory tetrominoFactory;
        TetrominoSpritePool tetrominoSpritePool;

        //Set Spawn connfigs
        public TetrominoSpawner(TetrominoSpawnConfig spawnConfig, Transform tetrominoParent, Transform spriteParent)
        {
            switch(spawnConfig.TetrominoSpawnType)
            {
                case Enums.TetrominoSpawnType.Random:
                tetrominoFactory = new RandomTetrominoFactory(spawnConfig.TetrominoPrefabs, spawnConfig.TetrominoPoolAmount, tetrominoParent);
                break;

                case Enums.TetrominoSpawnType.Queued:
                tetrominoFactory = new QueuedTetrominoFactory(spawnConfig.TetrominoPrefabs, spawnConfig.TetrominoPoolAmount, tetrominoParent);
                break;


            }

            tetrominoSpritePool = new TetrominoSpritePool(spawnConfig.TetrominoPrefabs, spawnConfig.TetrominoPoolAmount, spriteParent);
            
        }


        /// <summary>
        /// Create a tetromino and sprite pool
        /// </summary>
        public void CreatePool()
        {
            tetrominoFactory.CreatePool();
            tetrominoSpritePool.CreatePool();
        }


        /// <summary>
        /// Return a tetromino from the pool
        /// </summary>
        /// <returns></returns>
       public Tetromino GetTetromino()
        {
            return tetrominoFactory.GetTetromino();
        }

        /// <summary>
        /// Return the tetromino sprite with  given tetromino id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpriteRenderer GetTetrominoSprite(int id, bool isGhost = false)
        {
            return tetrominoSpritePool.GetTetrominoSprite(id, isGhost);
        }

        /// <summary>
        /// Return the tetromino sprite with  given tetromino type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SpriteRenderer GetTetrominoSprite(TetrominoType type, bool isGhost = false)
        {
            return tetrominoSpritePool.GetTetrominoSprite(type, isGhost);
        }


        
     
        
    }

}