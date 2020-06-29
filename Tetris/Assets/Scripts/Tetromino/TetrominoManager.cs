using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TetrominoSpawnSystem;
using Configs;

/// <summary>
/// Manager class used to communicate between Board and Tetromino Sprite Pool
/// </summary>
public class TetrominoManager : Configurable
{
    //Reference to tetromino spawner
    TetrominoSpawner tetrominoSpawner;
 
    //Transforms for cleaning up heirarchy
    private Transform tetrominoPoolParent;
    private Transform tetrominoSpritePoolParent;

    private TetrominoSpawnConfig spawnConfig;


    public TetrominoManager(BaseConfig _config) : base (_config)
    {
        spawnConfig = (TetrominoSpawnConfig) _config;
        tetrominoPoolParent = new GameObject("TetrominoPool").transform;
        tetrominoSpritePoolParent = new GameObject("TetrominoSpritePool").transform;
        tetrominoSpawner = new TetrominoSpawner(spawnConfig, tetrominoPoolParent, tetrominoSpritePoolParent);
        
        CreatePool();
    }

    /// <summary>
    /// This function calls the the create pool function to create a tetromino pool
    /// </summary>
    public void CreatePool()
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
        return sr;
    }


    /// <summary>
    /// This funnction retrieves a Tetromino from the spawner
    /// </summary>
    /// <returns></returns>
    public Tetromino GetTetromino()
    {
        Tetromino T = tetrominoSpawner.GetTetromino();
        T.gameObject.SetActive(true);
        T.transform.parent = null;
        return T;
    }


    public void DisableTetromino(Tetromino T)
    {
        if(T != null)
        {
            T.RemoveAllSprites();
            T.gameObject.SetActive(false);
            T.transform.parent = tetrominoPoolParent;
        }
        
    }

    public void DisableTetrominoSprite(SpriteRenderer sr)
    {
        sr.gameObject.SetActive(false);
        sr.transform.parent = tetrominoSpritePoolParent;
    }

   
}
