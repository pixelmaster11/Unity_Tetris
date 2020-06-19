using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrominoSpawnSystem
{   
    /// <summary>
    /// Base Tetromino Factory class  which provides methods for creation and delivery of tetrominos.
    /// This implements strategy pattern as the tetromino retrieval strategy is implemented by the child classes
    /// </summary>
    public abstract class TetrominoFactory 
    {   
        //Store the pooled objs
        protected List<Tetromino> pool;

        //Store possilbe objs that can be delivered from pool
        protected List<Tetromino> possible;

        //List of prefabs to create
        protected List<Tetromino> prefabs;

        //Amount to pool
        protected int amount;

        //Parent to which all pooled objs are nested
        protected Transform poolParent;

        /// <summary>
        /// This function is responsilbe for creating a pool of objs
        /// </summary>
        public abstract void CreatePool(); 

        /// <summary>
        /// This function is responsible for delivering a tetromino
        /// </summary>
        /// <returns></returns>
        public abstract Tetromino GetTetromino();

        /// <summary>
        /// This function creates a tetromino from one of the prefabs based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract Tetromino CreateTetromino(int id);
              
    }

}
