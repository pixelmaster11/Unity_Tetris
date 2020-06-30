using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A common interface for state machine states
/// </summary>
public interface IState
{
   void Entry();
   void StateUpdate();
   void Exit();
}
