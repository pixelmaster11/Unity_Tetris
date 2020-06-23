﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TetrominoSpawnSystem;
using Configs.TetrominoSO.SpawnSO;

/// <summary>
/// Manager class used to communicate between Board and Tetromino Sprite Pool
/// </summary>
public class TetrominoManager : MonoBehaviour
{
    //Reference to tetromino spawner
    TetrominoSpawner tetrominoSpawner;

    //Spawn configuraion Scriptable Object
    [SerializeField]
    private TetrominoSpawnConfig spawnConfig;
    
    //Transforms for cleaning up heirarchy
    [SerializeField]
    private Transform tetrominoPoolParent;

    [SerializeField]
    private Transform tetrominoSpritePoolParent;


    /// <summary>
    /// This function calls the the create pool function to create a tetromino pool
    /// </summary>
    public void CreatePool()
    {
        tetrominoSpawner = new TetrominoSpawner(spawnConfig, tetrominoPoolParent, tetrominoSpritePoolParent);
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
        
        T.RemoveAllSprites();
        T.gameObject.SetActive(false);
        T.transform.parent = tetrominoPoolParent;
    }

    public void DisableTetrominoSprite(SpriteRenderer sr)
    {
        sr.gameObject.SetActive(false);
        sr.transform.parent = tetrominoSpritePoolParent;
    }

 

}
