using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

/// <summary>
/// Base Tetromino class
/// </summary>
//TODO: Add Despawn Spawn Callbacks
public class Tetromino : MonoBehaviour
{

    //Reference to shared Tetromino Scriptable object configuration file
    [SerializeField]
    private TetrominoConfig tetrominoConfig;

    //List to store associated sprites with this tetromino
    [SerializeField]
    private List<SpriteRenderer> tetrominoSprites;
    
    //Current rotation of the tetromino
    [SerializeField]
    private int rotID;

    //Whether the piece is holded
    [SerializeField]
    private bool isHolded;

    //Access rotation id
    public int RotateID
    {
        get
        {
            return rotID;
        }

        private set{}
    }


    //Access Holded value
    public bool IsHolded
    {
        get
        {
            return isHolded;
        }

        private set
        {
           
        }
        
    }


    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
       isHolded = false;
    }

    
    /// <summary>
    /// Changes holded value 
    /// </summary>
    public void OnHold()
    {
        isHolded = !isHolded;
    }
  

    public List<SpriteRenderer> GetSprites()
    {
        return tetrominoSprites;
    }

   /// <summary>
   /// Function that saves a sprite associated to this tetromino
   /// </summary>
   /// <param name="sr"></param>
    public void SetSprite(SpriteRenderer sr)
    {
        if(!tetrominoSprites.Contains(sr))
        {
            tetrominoSprites.Add(sr);
        }
        
    }

  

    /// <summary>
    /// Function that removes a sprite associated with this tetromino
    /// </summary>
    /// <param name="sr"></param>
    public void RemoveSprite(SpriteRenderer sr)
    {
        if(tetrominoSprites.Count > 0)
        {
            tetrominoSprites.Remove(sr);
        }
    }

    public void RemoveAllSprites()
    {
        

        if(tetrominoSprites.Count > 0)
        {
            tetrominoSprites.Clear();
        }
    }


     /// <summary>
    /// Sets the rotation id based on rotation direction
    /// </summary>
    /// <returns></returns>
    public void SetTetrominoRotation(int rotDir)
    {
        rotID += rotDir;

        if(rotID > 3)
            rotID = 0;

        if(rotID < 0)
            rotID = 3;
    }

  
  
    /// <summary>
    /// Returns the 2D-Matrix from the config file
    /// </summary>
    /// <returns></returns>
    public int[,] GetTetrominoMatrix()
    {
        return tetrominoConfig.GetMatrix();
    }

    /// <summary>
    /// Returns the ID from config file
    /// </summary>
    /// <returns></returns>
    public int GetTetrominoID()
    {
        return tetrominoConfig.ID;
    }

    /// <summary>
    /// Retunrs the Type from config file
    /// </summary>
    /// <returns></returns>
    public TetrominoType GetTetrominoType()
    {
        return tetrominoConfig.TetrominoType;
    }

    /// <summary>
    /// Returns the Name from the config file
    /// </summary>
    /// <returns></returns>
    public string GetTetrominoName()
    {
        return tetrominoConfig.TetrominoName;
    }

    /// <summary>
    /// Returns the Sprite graphic from the config file
    /// </summary>
    /// <returns></returns>
    public Sprite GetTetrominoOriginalSprite()
    {
        return tetrominoConfig.TetrominoSprite;
    }


    /// <summary>
    /// Returns the Color from the config file
    /// </summary>
    /// <returns></returns>
    public Color32 GetTetrominoColor()
    {
        return tetrominoConfig.TetrominoColor;
    }


    /// <summary>
    /// Returns the Ghost Sprite graphic from the config file
    /// </summary>
    /// <returns></returns>
    public Sprite GetGhostSprite()
    {
        return tetrominoConfig.GhostSprite;
    }


    /// <summary>
    /// Returns the Ghost sprite Color from the config file
    /// </summary>
    /// <returns></returns>
    public Color32 GetGhostColor()
    {
        return tetrominoConfig.GhostColor;
    }

   
  
}
