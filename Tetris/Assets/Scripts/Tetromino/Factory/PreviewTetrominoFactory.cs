﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PreviewTetrominoFactory 
{
    //Store the pooled objs
    private List<PreviewTetromino> pool;

    //List of prefabs to create
    private List<PreviewTetromino> prefabs;

    //Amount to pool
    private int amount;

    //Parent to which all pooled objs are nested
    private Transform poolParent;
    
    public PreviewTetrominoFactory(List<PreviewTetromino> _prefabs, int _amount, Transform _parent)
    {
        prefabs = _prefabs;
        amount = _amount;
        poolParent = _parent;
    }


    public void CreatePool()
    {
        pool = new List<PreviewTetromino>();

        for(int i = 0; i < prefabs.Count; i++)
        {
            for(int k = 0; k < 3; k++)
            {
                PreviewTetromino p = Object.Instantiate(prefabs[i]);
                p.gameObject.SetActive(false);
                p.transform.parent = poolParent;
                pool.Add(p);
            }
        }
    }


    public PreviewTetromino GetPreview(int id)
    {
        PreviewTetromino p = pool.Find(x => x.ID == id && !x.gameObject.activeInHierarchy);

        if(p == null)
        {
            p = Object.Instantiate(prefabs.Find(x => x.ID == id));
            p.gameObject.SetActive(false);
            pool.Add(p);
        }

        return p;
    }
}
