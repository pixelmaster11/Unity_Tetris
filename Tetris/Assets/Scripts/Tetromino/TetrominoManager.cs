using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TetrominoSpawnSystem;
using Configs;


/// <summary>
/// Manager class used to handle Tetrominos and Tetromino Sprites
/// Spawning, Disabling / Enabling 
/// </summary>
public class TetrominoManager : Configurable
{
    //Reference to tetromino spawner
    TetrominoSpawner tetrominoSpawner;
 
    //Transforms for cleaning up heirarchy
    private Transform tetrominoPoolParent;
    private Transform tetrominoSpritePoolParent;

    //Cache spawn config file
    private TetrominoSpawnConfig spawnConfig;


    private List<SpriteRenderer> activeSprites;
    private Tetromino activeTetromino;

    public TetrominoManager(BaseConfig _config) : base (_config)
    {   
        //Set up Spawn system
        spawnConfig = (TetrominoSpawnConfig) _config;
        tetrominoPoolParent = new GameObject("TetrominoPool").transform;
        tetrominoSpritePoolParent = new GameObject("TetrominoSpritePool").transform;
        tetrominoSpawner = new TetrominoSpawner(spawnConfig, tetrominoPoolParent, tetrominoSpritePoolParent);
        
        activeSprites = new List<SpriteRenderer>();
        CreatePool();
    }


    public void Enable()
    {
        tetrominoSpawner.Enable();
    }

    /// <summary>
    /// This function calls the the create pool function to create a tetromino pool
    /// </summary>
    private void CreatePool()
    {      
        tetrominoSpawner.CreatePool();
    }

    /// <summary>
    /// This function retrieves a tetromino sprite by given ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(int ID, bool isGhost = false)
    {
        SpriteRenderer sr = tetrominoSpawner.GetTetrominoSprite(ID, isGhost);
        sr.sortingOrder = 1;
        activeSprites.Add(sr);
        return sr;
    }



    /// <summary>
    /// This function retrieves a tetromino sprite by given type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(TetrominoType type, bool isGhost = false)
    {
        SpriteRenderer sr = tetrominoSpawner.GetTetrominoSprite(type, isGhost);
        sr.sortingOrder = 1;
        activeSprites.Add(sr);
        return sr;
    }


    /// <summary>
    /// This funnction retrieves a Tetromino from the spawner
    /// </summary>
    /// <returns></returns>
    public Tetromino GetTetromino()
    {
        Tetromino T = tetrominoSpawner.GetTetromino();
        activeTetromino = T;
        T.gameObject.SetActive(true);
        T.transform.parent = null;
        
        return T;
    }


    
 


    /// <summary>
    /// Disables any active tetromino
    /// </summary>
    /// <param name="T">Tetromino to disable</param>
    public void DisableTetromino(Tetromino T)
    {   
        if(T != null)
        {
            T.RemoveAllSprites();

            if(!T.IsHolded)
            {
                
                T.OnDespawn();
                T.gameObject.SetActive(false);      
                T.transform.parent = tetrominoPoolParent;

            }
        }
    
  
    }

    /// <summary>
    /// Disable given tetromino sprite
    /// </summary>
    /// <param name="sr"></param>
    public void DisableTetrominoSprite(SpriteRenderer sr)
    {
        sr.gameObject.SetActive(false);
        sr.transform.parent = tetrominoSpritePoolParent;

        if(activeSprites.Contains(sr))
        {
            activeSprites.Remove(sr);
        }
    }



    public void OnGameOver()
    {
        foreach(SpriteRenderer sr in activeSprites)
        {
            sr.gameObject.SetActive(false);
            sr.transform.parent = tetrominoSpritePoolParent;
        }

        activeSprites.Clear();

        if(activeTetromino != null)
        {
            DisableTetromino(activeTetromino);
            activeTetromino = null;
        }

        tetrominoSpawner.OnGameOver();
    }


   
   
}
