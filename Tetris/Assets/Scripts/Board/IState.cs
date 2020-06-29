using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
   void Entry();
   void StateUpdate();
   void Exit();
}
