﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InputConfig/Keyboard", order = 1)]
public class KeyboardInputConfig : InputConfig
{
   
   [SerializeField]
   private KeyCode moveLeft = KeyCode.A;

   [SerializeField]
   private KeyCode moveRight = KeyCode.D;

   [SerializeField]
   private KeyCode moveDown  = KeyCode.S;

   [SerializeField]
   private KeyCode rotateLeft = KeyCode.Q;

   [SerializeField]
   private KeyCode rotateRight = KeyCode.E;

   [SerializeField]
   private KeyCode snap = KeyCode.W;

   public KeyCode MoveLeft
   {
      get
      {
          return moveLeft;
      }
   }

    public KeyCode MoveRight
    {
      get
      {
          return moveRight;
      }
    }

    public KeyCode MoveDown
    {
      get
      {
          return moveDown;
      }
    }

    public KeyCode RotateLeft
    {
      get
      {
          return rotateLeft;
      }
    }

    public KeyCode RotateRight
    {
      get
      {
          return rotateRight;
      }
    }

    public KeyCode Snap
    {
      get
      {
          return snap;
      }
    }

}
