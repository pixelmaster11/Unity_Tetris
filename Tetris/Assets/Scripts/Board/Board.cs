using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    
   [SerializeField]
    private BoardConfig boardConfig;

    private int [,] logicalBoard;  
    private SpriteRenderer[,] graphicalBoard;

    
    private int width, height;

    [SerializeField]
    // Initialize to topmost row mid column 
    int currentPosX = 0; 


    [SerializeField]
    // Initialize to topmost row
    int currentPosY = 0; 

    int [,] currPiece;

    [SerializeField]
    TetrominoManager tetrominoManager;

    Tetromino T;

    private void Start()
    {
        width = boardConfig.width;
        height = boardConfig.height;

        logicalBoard = new int[width, height];
        graphicalBoard = new SpriteRenderer[width, height];

        tetrominoManager.CreatePool();

        InitializeBoard();

    
    }

    private void Update()
    {
        GetInputs();
  
    }
    
    private void GetInputs()
    {

        //Move Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Move(-1, 0))
            {
                currentPosX -= 1;

                DisplayBoard();
                DisplayPiece();
            }

        }


        //Move Right
        if (Input.GetKeyDown(KeyCode.D))
        {

            if (Move(1, 0))
            {
                currentPosX += 1;

                DisplayBoard();
                DisplayPiece();
            }

        }


        //Move Down
        if (Input.GetKeyDown(KeyCode.S))
        {
           

            if (Move(0, -1))
            {
                currentPosY -= 1;

                DisplayBoard();
                DisplayPiece();
            }


        }

    }


   

    private void InitializeBoard()
    {
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //If boundary
                if(y == 0 || x == 0 || x == (width - 1))
                {
                    Instantiate(boardConfig.boundarySprite, new Vector2(x + x * boardConfig.offX, y + y * boardConfig.offY), Quaternion.identity).transform.parent = this.transform;

                    graphicalBoard[x, y] = null;
                    logicalBoard[x, y] = 9;
                } 

                //BG tile
                else
                {
                    Instantiate(boardConfig.backgroundSprite, new Vector2(x + x * boardConfig.offX, y + y * boardConfig.offY), Quaternion.identity).transform.parent = this.transform;
                    graphicalBoard[x, y] = null;
                    logicalBoard[x, y] = 100;
                }
               
              
                
            }
        }


       
        //Middle column of the board
        currentPosX = width / 2 - 1;

        //Topmost row of the board
        currentPosY = height / 2 + 1;

        //T = new Tetromino(1);
        T = tetrominoManager.GetTetromino();

        DisplayPiece();

    }


  
    /// <summary>
    /// This function displays only the locked Tetromino Sprites and removes any not locked Tetromino Sprites
    /// </summary>
    private void DisplayBoard()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
               
                //Tetromino
                if (logicalBoard[x, y] != 100 && logicalBoard[x,y] != 9)
                {                                  
                    //graphicalBoard[x, y] = tetrominoManager.GetTetrominoSprite(logicalBoard[x,y]);
                    /*if(graphicalBoard[x, y] != null && !graphicalBoard[x,y].gameObject.activeInHierarchy)
                    {
                        graphicalBoard[x,y].gameObject.SetActive(true);
                    }*/ 
                    
                }

                //Boundary
                else if (logicalBoard[x, y] == 9)
                {
                    //graphicalBoard[x, y].color = boundaryColor;
                }

                //Empty Space and to disable previous tetromino sprites before tetromino is locked
                else if (logicalBoard[x, y] == 100)
                {  
                    //If there is a tetromino sprite and it is not locked. Remove it
                    if(graphicalBoard[x, y] != null && graphicalBoard[x,y].gameObject.activeInHierarchy)
                    {
                        graphicalBoard[x, y].gameObject.SetActive(false);
                        graphicalBoard[x,y] = null;
                    }                  
                    
                }



            }
        }
    }


    //Displays or locks pieces on board
    private void DisplayPiece()
    {
        currPiece = T.GetTetrominoMatrix(); //tetrominoManager.GetTetrominoMatrix(T.tetrominoConfig.ID);

    
      //GEtLength(0) = Rows / Height
        for (int x = 0; x < currPiece.GetLength(1); x++)
        {
            for (int y = 0; y < currPiece.GetLength(0); y++)
            {       
                    
                    //Piece position on board
                    //TUT: EXPLAIN THIS
                    int boardX = x + currentPosX ; //+ currentPosY;
                    int boardY = y + currentPosY; //+ currentPosX;

                //Check if inside boundaries, otherwise don't do anything
                //TUT: EXPLAIN WHAT HAPPENS IF OUTSIDE BOUNDARY
                //wE SIMPLY DO NOT CARE
                    if (boardX > 0 && boardX < (width - 1))
                    {
                        if (boardY > 0 && boardY < height)
                        {
                            //If its a tetromino position from tetromino matrix
                            //TUT: IMPORTANCE OF ISLOCKED
                            //SHOW USING COROUTINES THAT THE LOCKED PIECES DEOS NOT GET WIPED OUT AS IT IS DISPLAYED  CONSTANTLY USING THIS FUNCTION
                            //WE ONLY NEED TO DISPLAY THE PIECE GRAPHICALLY TILL ITS LOCKED LOGICALLY
                            //ONCE ITS LOGICALLY LOCKED THE BOARD WILL DISPLAY THE PIECE
                            if (currPiece[y, x] == 1 && graphicalBoard[boardX, boardY] == null)  //&& !isLocked)
                            {    
                                                     
                                //Display only piece graphic                       
                                //graphicalBoard[pieceOnBoardX, pieceOnBoardY].color = tetrominos[randomPieceID].tetrominoColor;
                              
                               //Get the sprite associated with current Tetromino piece
                                SpriteRenderer currPieceSprite = tetrominoManager.GetTetrominoSprite(T.GetTetrominoID());

                                
                                currPieceSprite.gameObject.transform.position = new Vector2(boardX + boardX * boardConfig.offX,
                                                                                            boardY + boardY * boardConfig.offY);

                                currPieceSprite.gameObject.SetActive(true);

                                currPieceSprite.transform.parent = T.transform;

                                //TODO: Add the sprites ones a piece is locked
                                //T.tetrominoSprites.Add(currPieceSprite);

                                graphicalBoard[boardX, boardY] = currPieceSprite;
                                
                                
                            }


                          
                        }
                    }
                                                          
            }
        }
    }


    
    //Can we fit our piece after doing the next move 
    private bool Move(int xDir, int yDir)
    {
        //next piece positions to be performed
        int nextPosX = currentPosX + xDir;
        int nextPosY = currentPosY + yDir;

        for (int x = 0; x < currPiece.GetLength(1); x++)
        {
            for (int y = 0; y < currPiece.GetLength(0); y++)
            {
                //Move next piece positions on board
                int pieceOnBoardX = x + nextPosX;
                int pieceOnBoardY = y + nextPosY;           

                //If next positions are inside the boundary
                if (pieceOnBoardX >= 0 && pieceOnBoardX < width)
                {
                    if (pieceOnBoardY >= 0 && pieceOnBoardY < height)
                    {
                        //Cannot fit piece
                        //TUT: EXPLAIN WHY CANNOT FIT?
                        //WE ARE CHECKING PIECE MATRIX POSITIONS AND PIECE ON BOARD POSITIONS
                        if (currPiece[y, x] != 0 && logicalBoard[pieceOnBoardX, pieceOnBoardY] != 100)
                        {
                            return false;
                        }
                    }
                }

            }
        }

        return true;
    }

}
