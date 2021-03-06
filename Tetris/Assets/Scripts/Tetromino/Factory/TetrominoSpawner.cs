﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace TetrominoSpawnSystem
{   
    /// <summary>
    /// Responsible for spawning tetrominos using set spawn method
    /// </summary>
    public class TetrominoSpawner 
    {
        //Reference to Tetromino Factory and Sprite Factory
        TetrominoFactory tetrominoFactory;
        TetrominoSpritePool tetrominoSpritePool;
    

        TetrominoSpawnConfig config;
        private Queue <Tetromino> spawnQueue;

        

        //Set Spawn connfigs
        public TetrominoSpawner(TetrominoSpawnConfig spawnConfig, Transform tetrominoParent, Transform spriteParent)
        {   
            config = spawnConfig;

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
            CreateSpawnQueue();
        }


        /// <summary>
        /// Return a tetromino from the pool
        /// </summary>
        /// <returns></returns>
       public Tetromino GetTetromino()
        {

            //return tetrominoFactory.GetTetromino();
            Tetromino tetromino = spawnQueue.Dequeue();

            FillSpawnQueue();

            return tetromino;
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


        /// <summary>
        /// Creates a spawn queue with maximum allowed initial pieces 
        /// Also used to display UI for next piece preview
        /// </summary>
        private void CreateSpawnQueue()
        {
            spawnQueue = new Queue<Tetromino>();

            for(int i = 0; i < config.MaxTetrominosInQueue; i++)
            {
                FillSpawnQueue();
            }

            
        }

        /// <summary>
        /// Gets a tetromino from factory and lines it up in the spawn queue
        /// </summary>
        private void FillSpawnQueue()
        {
            Tetromino t = tetrominoFactory.GetTetromino();

            //Tetromino is enqueued
            t.OnSpawn();
           
            spawnQueue.Enqueue(t);                      

            //Raise spawn event for UI purposes
            if(EventManager.TetrominoSpawnEvent != null)
            {
                EventManager.TetrominoSpawnEvent(t);
            }
            
        }   
        
    }

}