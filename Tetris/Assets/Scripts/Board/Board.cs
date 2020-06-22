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

    [SerializeField]
    int ghostPosX  = 0;

    [SerializeField]
    int ghostPosY = 0;

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
            if (CanMove(-1, 0))
            {
                currentPosX -= 1;

                DisplayBoard();
                DisplayPiece(currentPosX, currentPosY);
                Ghost();
            }

        }


        //Move Right
        if (Input.GetKeyDown(KeyCode.D))
        {

            if (CanMove(1, 0))
            {
                currentPosX += 1;

                DisplayBoard();
                DisplayPiece(currentPosX, currentPosY);
                Ghost();
            }

        }


        //Move Down
        if (Input.GetKeyDown(KeyCode.S))
        {
           

            if (CanMove(0, -1))
            {
                currentPosY -= 1;

                DisplayBoard();
                DisplayPiece(currentPosX, currentPosY);
                Ghost();
            }


        }



         //Rotate Piece AC
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (CanRotate(1))
            {

                DisplayBoard();
                DisplayPiece(currentPosX, currentPosY);
                 Ghost();
            }


        }


        //Rotate Piece CC
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CanRotate(-1))
            {
                DisplayBoard();
                DisplayPiece(currentPosX, currentPosY);
                Ghost();

            }


        }

        if(Input.GetKeyDown(KeyCode.W))
        {   
            DisplayBoard();
            SnapToGhost();
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
        currentPosX = 0;//width / 2 - 1;

        //Topmost row of the board
        currentPosY = height / 2 + 1;

        //T = new Tetromino(1);
        T = tetrominoManager.GetTetromino();
        currPiece = T.GetTetrominoMatrix(); 

        DisplayPiece(currentPosX, currentPosY);
        Ghost();

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
    private void DisplayPiece(int currPosX, int currPosY, bool isGhost = false)
    {
        
    
      //GEtLength(0) = Rows / Height
        for (int x = 0; x < currPiece.GetLength(1); x++)
        {
            for (int y = 0; y < currPiece.GetLength(0); y++)
            {       
                    
                    //Piece position on board
                    //TUT: EXPLAIN THIS
                    int boardX = x + currPosX;//currentPosX; 
                    int boardY = y + currPosY; //currentPosY; 

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

                                SpriteRenderer currPieceSprite;

                               
                                //Get the sprite associated with current Tetromino piece
                                currPieceSprite = tetrominoManager.GetTetrominoSprite(T.GetTetrominoID(), isGhost);

                                
                                currPieceSprite.gameObject.transform.position = new Vector2(boardX + boardX * boardConfig.offX,
                                                                                            boardY + boardY * boardConfig.offY);

                                currPieceSprite.gameObject.SetActive(true);

                                currPieceSprite.transform.parent = T.transform;

                                //TODO: Add the sprites ones a piece is locked
                                graphicalBoard[boardX, boardY] = currPieceSprite;

                               
                    
                                                          
                                
                            }


                          
                        }
                    }
                                                          
            }
        }
    }


    
    //Can we fit our piece after doing the next move 
    private bool CanMove(int xDir, int yDir)
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


    private void SnapToGhost()
    {   

        currentPosX = ghostPosX;
        currentPosY = ghostPosY;
        DisplayPiece(ghostPosX, ghostPosY);
        //TODO: Lock Piece after snap
    }

     //DisplayGhost Piece
    private void Ghost()
    {      
        int rd = 1;
        while(CanMove(0, -rd))
        {        
            rd ++;        
        }

        if(rd > 1)
        {
            ghostPosX = currentPosX;
            ghostPosY = currentPosY + 1 - rd;
            DisplayPiece(ghostPosX, ghostPosY, true);
        }
       
              
    }



   //Can we fit our piece after rotating?
    private bool CanRotate(int rotDirection)
    {   
        

        //Rotate the piece
        int [,] rotatedPiece = Rotate(currPiece, rotDirection);

        //Check if rotated piece can fit on board
        for (int x = 0; x < rotatedPiece.GetLength(0); x++)
        {
            for (int y = 0; y < rotatedPiece.GetLength(1); y++)
            {
                int pieceOnBoardX = x + currentPosX;
                int pieceOnBoardY = y + currentPosY;

                //If inside boundary
                if (pieceOnBoardX >= 0 && pieceOnBoardX < width)
                {
                    if (pieceOnBoardY >= 0 && pieceOnBoardY < height)
                    {
                        //Cannot fit
                        //If it is a sprite in the tetromino matrix and the board is not empty (Either other sprites or boundary is present)
                        if (rotatedPiece[y, x] == 1 && logicalBoard[pieceOnBoardX, pieceOnBoardY] != 100)
                        {
                            
                            return false;
                        }
                    }
                }

            }
        }

        //Piece can fit succesfully
        //Assign current piece as rotated piece
        currPiece = rotatedPiece;
        return true;
    }


    //Rotate by 90 deg in CC and AC
    private int[,] Rotate (int[,] piece, int direction)
    {
      
        int dim = piece.GetLength(0);
       
        //TUT: EXPLAIN ROTATION
        int[,] npiece = new int[dim, dim];


        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                if (direction == 1)
                {
                   npiece[i, j] = piece[dim - 1 - j, i];
              
                }
                   
                else
                {
                    npiece[i, j] = piece[j, dim - 1 - i];  
               
                }
                    
            }

        }


        return npiece;
    }
    
    //Make pieces fall automatically
   /* private void AutoFall()
    {
        /*if(piecesLocked % 10 == 0)
        {
           // fallGap -= 0.2f;
        }

        //Wait for desired time
        if (timer <= fallGap)
        {
            //TUT: EXPLAIN TIME.DELTA TIME IMPORTANCE
            timer += Time.deltaTime;
        }

        else
        {
            //check if piece can fit one step down
            //TUT: ISLOCKED IMPORTANCE
            //PIECES NEED TO AUTOFALL ONLY IF THEY ARE NOT LOCKED
            //MAINLY FOR DEBUGGING PURPOSES
            if (CheckCanFitNextMove(0, -1) && !isLocked)
            {
                currentPosY -= 1;
              
            }

            //If it cannot stop and lock the piece on board
            else
            {
                if(!isDebug)
                StopAutoFall();
            }

            timer = 0;
        }

    }*/


}
