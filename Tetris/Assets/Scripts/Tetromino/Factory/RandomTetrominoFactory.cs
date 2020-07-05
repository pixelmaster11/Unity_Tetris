using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrominoSpawnSystem
{
    /// <summary>
    /// This class is responsible for Randomly delivering tetrominos
    /// </summary>
    public class RandomTetrominoFactory : TetrominoFactory
    {

        //Init
        public RandomTetrominoFactory(List<Tetromino> _prefabs, int _amount, Transform _parent)
        {
            prefabs = _prefabs;
            amount = _amount;
            poolParent = _parent;

            //Random seed
            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        /// <summary>
        /// Creates a pool
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
        /// Delivers a random tetromino
        /// </summary>
        /// <returns></returns>
        public override Tetromino GetTetromino()
        {   
            
            int randID = Random.Range(0, prefabs.Count);

            possible = pool.FindAll(x => !x.gameObject.activeSelf && x.GetTetrominoID() == randID && !x.IsHolded);

            if(possible.Count > 0)
            {   
                Tetromino obj = possible[0];
                possible.Clear();

                return obj;
            }

            else
            {
                
                Tetromino obj = CreateTetromino(randID);
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
