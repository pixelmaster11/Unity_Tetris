using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
   

    [SerializeField]
    List<Updateable> updateables;

    private void Awake()
    {
        
      
    }


    private void Update()
    {   
        for(int i = 0; i < updateables.Count; i++)
        {
            if(updateables[i] != null)
            {
                updateables[i].Tick();
            }
        }
    }
}
