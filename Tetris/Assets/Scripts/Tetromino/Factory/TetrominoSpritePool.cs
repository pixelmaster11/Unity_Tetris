using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

/// <summary>
/// Class is responsible to create a tetromino sprite pool
/// </summary>
public class TetrominoSpritePool
{   
    //List to hold all tetrominos
    private List<Tetromino> tetrominos;

    //Pool amount
    private int amount;

    //Dictionary to Retrieve a tetromino Sprite from the available pool  using the tetromino type
    private Dictionary<TetrominoType, List<SpriteRenderer>> pool = new Dictionary<TetrominoType, List<SpriteRenderer>>();

    //Transform to parent all the pooled sprites
    private Transform poolParent;

    
  

    //Initialize
    public TetrominoSpritePool(List<Tetromino> _tetrominos, int _amount, Transform _parent)
    {
        tetrominos = _tetrominos;
        amount = _amount;
        poolParent = _parent;
    }
  
    /// <summary>
    /// This function creates a pool of tetromino sprites for all available tetrominos
    /// </summary>
    public void CreatePool()
    {

        for(int k = 0; k < tetrominos.Count; k++)
        {
            Tetromino t = tetrominos[k];
            List<SpriteRenderer> srList = new List<SpriteRenderer>();

            for (int i = 0; i < amount; i++)
            {   
                SpriteRenderer sr = CreateSprite(k);
                srList.Add(sr);

            }

            pool.Add(t.GetTetrominoType(), srList);
        }
        
    }

    /// <summary>
    /// This function Returns a tetromino sprite of given ID
    /// </summary>
    /// <param name="ID">Tetromino ID</param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(int ID)
    {
         List<SpriteRenderer> srList; 
         

        TetrominoType type = (TetrominoType) ID;

         if(!pool.TryGetValue(type, out srList))
         {
             throw new KeyNotFoundException();
         }


        for(int i = 0; i < srList.Count; i++)
        {
            if(!srList[i].gameObject.activeInHierarchy)
            {   
                //srList[i].gameObject.SetActive(true);
                return srList[i];
            }
        }

        SpriteRenderer sr = CreateSprite(ID);
        srList.Add(sr);

        return sr;
    }

    /// <summary>
    /// This function returns a tetromino sprite of a given type
    /// </summary>
    /// <param name="type">Tetromino type</param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(TetrominoType type)
    {
         List<SpriteRenderer> srList; 
         

         int ID = (int) type;

         if(!pool.TryGetValue(type, out srList))
         {
             throw new KeyNotFoundException();
         }


        for(int i = 0; i < srList.Count; i++)
        {
            if(!srList[i].gameObject.activeInHierarchy)
            {   
                //srList[i].gameObject.SetActive(true);
                return srList[i];
            }
        }
       
       SpriteRenderer sr = CreateSprite(ID);
       srList.Add(sr);
       return sr;

    }


    /// <summary>
    /// Creates a tetromino sprite from the given Tetromino ID
    /// </summary>
    /// <param name="ID">Tetromino ID</param>
    /// <returns></returns>
    private SpriteRenderer CreateSprite(int ID)
    {
        GameObject go = new GameObject(tetrominos[ID].GetTetrominoName());
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = tetrominos[ID].GetTetrominoSprite();
        sr.color = tetrominos[ID].GetTetrominoColor();
        sr.gameObject.SetActive(false);
        sr.transform.parent = poolParent;
        return sr;
    }

}
