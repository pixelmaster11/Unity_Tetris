using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

public class Board : Configurable
{
   
    public Board(BaseConfig _config) : base(_config)
    {
        boardConfig = (BoardConfig) _config;

        boardParent = new GameObject("Board").transform;
        boardSprites = new GameObject("BoardSprites").transform;
        boardTetrominos = new GameObject("BoardTetrominos").transform;
        boardSprites.parent = boardParent;
        boardTetrominos.parent = boardParent;
    }

    public BoardConfig boardConfig;

    public int[,] logicalBoard;
    public SpriteRenderer [,] graphicalBoard;

    public int currentPosX, currentPosY;

    public Tetromino tetromino;
    public int [,] currPiece;



    private Transform boardParent;
    public Transform boardSprites;
    public Transform boardTetrominos;




    
}



