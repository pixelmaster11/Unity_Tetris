using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Configs.TetrominoSO
{

    /// <summary>
    /// Base class for Tetromino configuration data.
    /// The reason for this SO is that this data will be shared by all instances including the inherited tetromino matrix thus saving lot of memory.
    /// This is a type of Flyweight pattern where this tetromino data will be shared by as many Tetromino instances.
    /// </summary>
    public abstract class TetrominoConfig : ScriptableObject
    {
        //Sprite associated with the Tetromino
        [SerializeField]
        protected Sprite sprite;

        //Tetromino ID
        [SerializeField]
        protected int id;

        //Tetrromino Type
        [SerializeField]
        protected TetrominoType type;

        //Tetromino name
        [SerializeField]
        protected string tetrominoName;

        //Color for Tetromino Sprite
        [SerializeField]
        protected Color32 color;


        public Sprite TetrominoSprite
        {
            get
            {
                return sprite;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string TetrominoName
        {
            get
            {
                return tetrominoName.ToUpper();
            }
        }

        public Color32 TetrominoColor
        {
            get
            {
                return color;
            }
        }


        public TetrominoType TetrominoType
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Abstract method which returns the 2D-matrix of the Tetromino. 
        /// </summary>
        /// <returns></returns>
        public abstract int[,] GetMatrix();
        
    }
}



