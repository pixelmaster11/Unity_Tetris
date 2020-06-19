using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> where T:MonoBehaviour
{

    private List<T> pool;
    private List<T> possible;
    private List<T> prefabs;
    private int amount;
    private Transform poolParent;

    public GenericPool(List<T> _prefabs, int _amount, Transform _parent)
    {
        prefabs = _prefabs;
        amount = _amount;
        poolParent = _parent;
    }

 
    public void CreatePool() 
    {   
        for(int k = 0; k < prefabs.Count; k++)
        {
            for (int i = 0; i < amount; i++)
            {             
                pool.Add(CreateObj(k));             
            }
        }  
        

    }


    public T GetObjectRandom()
    {   
        //Random seed
        Random.InitState((int)System.DateTime.Now.Ticks);
        int rand = Random.Range(0, prefabs.Count);

        possible = pool.FindAll(x => !x.gameObject.activeSelf);

        if(possible.Count > 0)
        {   
            T obj = possible[rand];
            possible.Clear();

            return obj;
        }

        else
        {
            
            T obj = CreateObj(rand);
            pool.Add(obj);
            return obj;
        }
  

    }

    public T GetObject(int id = -1)
    {   
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }

        if(id == -1)
        {
             Random.InitState((int)System.DateTime.Now.Ticks);
             id = Random.Range(0, prefabs.Count);
        }
       
        T obj = CreateObj(id);
        pool.Add(obj);
        return obj;
        

    }


    private T CreateObj(int id)
    {
         T obj = Object.Instantiate(prefabs[id]);
         obj.gameObject.SetActive(false);
         obj.transform.parent = poolParent;
         return obj;
    }
   
}
