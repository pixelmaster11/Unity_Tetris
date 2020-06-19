using System.Collections;
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
    public SpriteRenderer GetTetrominoSprite(int ID)
    {
        return tetrominoSpawner.GetTetrominoSprite(ID);
    }



    /// <summary>
    /// This function retrieves a tetromino sprite by given type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(TetrominoType type)
    {
        return tetrominoSpawner.GetTetrominoSprite(type);
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

 

}
