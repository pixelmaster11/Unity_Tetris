using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs.TetrominoSO.SpawnSO;

namespace TetrominoSpawnSystem
{

    public class TetrominoSpawner 
    {
        
        TetrominoFactory tetrominoFactory;
        TetrominoSpritePool tetrominoSpritePool;


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


        public void CreatePool()
        {
            tetrominoFactory.CreatePool();
            tetrominoSpritePool.CreatePool();
        }

        private void CreateTetrominoPool()
        {
            tetrominoFactory.CreatePool();
        }


        private void CreateTetrominoSpritePool()
        {
            tetrominoSpritePool.CreatePool();
        }

        public Tetromino GetTetromino()
        {
            return tetrominoFactory.GetTetromino();
        }

        public SpriteRenderer GetTetrominoSprite(int id)
        {
            return tetrominoSpritePool.GetTetrominoSprite(id);
        }

        public SpriteRenderer GetTetrominoSprite(TetrominoType type)
        {
            return tetrominoSpritePool.GetTetrominoSprite(type);
        }

        
    }

}