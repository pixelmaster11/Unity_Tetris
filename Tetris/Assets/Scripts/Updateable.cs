using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Updateable : MonoBehaviour, IUpdateable
{
    public abstract void Tick();
  
}
