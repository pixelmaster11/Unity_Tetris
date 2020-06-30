using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// This class represents all the Board data
    /// </summary>
    public class Board : Configurable
    {
        
        public Board(BaseConfig _config) : base(_config)
        {   
            //Cache config
            boardConfig = (BoardConfig) _config;

            //Create empty transforms for better scene hierarchy
            boardParent = new GameObject("Board").transform;
            boardSprites = new GameObject("BoardSprites").transform;
            boardTetrominos = new GameObject("BoardTetrominos").transform;
            boardSprites.parent = boardParent;
            boardTetrominos.parent = boardParent;
        }

        //Reference to Board config file
        public BoardConfig boardConfig;

        //Arrays to hold board
        public int[,] logicalBoard;
        public SpriteRenderer [,] graphicalBoard;

        //Track of Tetromino on board
        public int currentPosX, currentPosY;

        //Active tetromino on board
        public Tetromino tetromino;

        //Current matrix of tetromino on board, can be manipulated to move / rotate, etc
        public int [,] currPiece;

        //Empty Transforms for cleaning up the scene hierarchy
        private Transform boardParent;
        public Transform boardSprites;
        public Transform boardTetrominos;




        
    }

}

