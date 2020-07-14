using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.Experimental.Rendering.Universal;


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

     //Dictionary to Retrieve a tetromino Sprite from the available pool  using the tetromino type
    private Dictionary<TetrominoType, List<SpriteRenderer>> ghostPool = new Dictionary<TetrominoType, List<SpriteRenderer>>();


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
            List<SpriteRenderer> ghList = new List<SpriteRenderer>();

            for (int i = 0; i < amount; i++)
            {   
                SpriteRenderer sr = CreateSprite(k);
                srList.Add(sr);

                SpriteRenderer gh_sr = CreateGhostSprite(k);
                ghList.Add(gh_sr);

            }

            pool.Add(t.GetTetrominoType(), srList);
            ghostPool.Add(t.GetTetrominoType(), ghList);
        }
        
    }

    /// <summary>
    /// This function Returns a tetromino sprite of given ID
    /// </summary>
    /// <param name="ID">Tetromino ID</param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(int ID, bool ghost = false)
    {
        List<SpriteRenderer> srList;   
        TetrominoType type = (TetrominoType) ID;

        if(ghost)
        {
            if(!ghostPool.TryGetValue(type, out srList))
            {
                throw new KeyNotFoundException();
            }
        }

        else
        {
            if(!pool.TryGetValue(type, out srList))
            {
                throw new KeyNotFoundException();
            }
        }


        for(int i = 0; i < srList.Count; i++)
        {
            if(!srList[i].gameObject.activeInHierarchy)
            {   
                //srList[i].gameObject.SetActive(true);
                return srList[i];
            }
        }

        if(ghost)
        {
            SpriteRenderer gh_sr = CreateGhostSprite(ID);
            srList.Add(gh_sr);
            return gh_sr;
        }

        else
        {
            SpriteRenderer sr = CreateSprite(ID);
            srList.Add(sr);
            return sr;
        }
        
    }

    /// <summary>
    /// This function returns a tetromino sprite of a given type
    /// </summary>
    /// <param name="type">Tetromino type</param>
    /// <returns></returns>
    public SpriteRenderer GetTetrominoSprite(TetrominoType type, bool ghost = false)
    {
         List<SpriteRenderer> srList; 
         int ID = (int) type;

        if(ghost)
        {
            if(!ghostPool.TryGetValue(type, out srList))
            {
                throw new KeyNotFoundException();
            }
        }

        else
        {
            if(!pool.TryGetValue(type, out srList))
            {
                throw new KeyNotFoundException();
            }
        }


        for(int i = 0; i < srList.Count; i++)
        {
            if(!srList[i].gameObject.activeInHierarchy)
            {   
                //srList[i].gameObject.SetActive(true);
                return srList[i];
            }
        }
       
        if(ghost)
        {
            SpriteRenderer gh_sr = CreateGhostSprite(ID);
            srList.Add(gh_sr);
            return gh_sr;
        }

        else
        {
            SpriteRenderer sr = CreateSprite(ID);
            srList.Add(sr);
            return sr;
        }

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
       
        sr.sprite = tetrominos[ID].GetTetrominoOriginalSprite();
        sr.color = tetrominos[ID].GetTetrominoColor();

        /*Light2D light2D = go.AddComponent<Light2D>();
        light2D.lightType = Light2D.LightType.Point;
        light2D.color = tetrominos[ID].GetGhostColor();
        light2D.pointLightInnerRadius = 1;
        light2D.pointLightOuterRadius = 2;
        light2D.intensity = 0.3f;*/
        
        
        sr.gameObject.SetActive(false);
        sr.transform.parent = poolParent;
        return sr;
    }


    /// <summary>
    /// Creates a Ghost sprite from the given Tetromino ID
    /// </summary>
    /// <param name="ID">Tetromino ID</param>
    /// <returns></returns>
    private SpriteRenderer CreateGhostSprite(int ID)
    {
        GameObject go = new GameObject(tetrominos[ID].GetTetrominoName());
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = tetrominos[ID].GetGhostSprite();
        sr.color = tetrominos[ID].GetGhostColor();

        
        /*Light2D light2D = go.AddComponent<Light2D>();
        light2D.lightType = Light2D.LightType.Point;
        light2D.color = tetrominos[ID].GetGhostColor();
        light2D.pointLightInnerRadius = 1;
        light2D.pointLightOuterRadius = 2;*/
        
        
        sr.gameObject.SetActive(false);
        sr.transform.parent = poolParent;
        return sr;
    }

}
