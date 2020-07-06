using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrominoSpawnSystem
{   
    /// <summary>
    /// A queued tetromino delivery factory responsible for delivering tetrominos based on the provided Queue
    /// </summary>
    public class QueuedTetrominoFactory : TetrominoFactory
    {

        Queue<int> tetrominoQueue;
        
        //Init
        public QueuedTetrominoFactory(List<Tetromino> _prefabs, int _amount, Transform _parent)
        {
            prefabs = _prefabs;
            amount = _amount;
            poolParent = _parent;
            tetrominoQueue = new Queue<int>( new [] {1 , 0 , 1, 1, 0}); 
        }

        //Init with provided Queue
        public QueuedTetrominoFactory(Queue<int> _queue, List<Tetromino> _prefabs, int _amount, Transform _parent)
        {
            prefabs = _prefabs;
            amount = _amount;
            poolParent = _parent;
            tetrominoQueue = _queue;
        }

        /// <summary>
        /// Create a pool
        /// </summary>
        public override void CreatePool() 
        {   
            pool = new List<Tetromino>();

            for(int k = 0; k < prefabs.Count; k++)
            {
                for (int i = 0; i < amount; i++)
                {             
                    pool.Add(CreateTetromino(k));             
                }
            }  
            
        }


        /// <summary>
        /// Deliver Tetromino based on the Queue index
        /// </summary>
        /// <returns></returns>
        public override Tetromino GetTetromino()
        {   
    
            int queuedID = tetrominoQueue.Dequeue();
            tetrominoQueue.Enqueue(queuedID);

            possible = pool.FindAll(x => !x.gameObject.activeSelf && x.GetTetrominoID() == queuedID && 
                                    !x.IsHolded && !x.IsSpawned);

            if(possible.Count > 0)
            {   
                Tetromino obj = possible[0];
                possible.Clear();

                return obj;
            }

            else
            {
                
                Tetromino obj = CreateTetromino(queuedID);
                pool.Add(obj);
                return obj;
            }
    

        }


        protected override Tetromino CreateTetromino(int id)
        {    
            for(int i = 0; i < prefabs.Count; i++)
            {
                if(prefabs[i].GetTetrominoID() == id)
                {
                    Tetromino obj = Object.Instantiate(prefabs[id]);
                    obj.gameObject.SetActive(false);
                    obj.transform.parent = poolParent;
                    return obj;
                }
            }
            
            throw new System.Exception("No Tetrominos Match with Provided ID"+ id);
        }
    }
}

