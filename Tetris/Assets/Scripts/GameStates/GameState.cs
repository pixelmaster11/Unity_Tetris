using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
   public abstract void Entry();
   public abstract void StateUpdate();
   public abstract void Exit();

   
}
