using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : Updateable
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

    float timer;


    private void OnEnable()
    { 
        EventManager.MoveEvent += MoveIssued;
        EventManager.RotateEvent += RotateIssued;
        EventManager.SnapEvent += SnapIssued;
    }

    private void OnDisable()
    {
      
        EventManager.MoveEvent -= MoveIssued;
        EventManager.RotateEvent -= RotateIssued;
        EventManager.SnapEvent -= SnapIssued;
    }

    private void Start()
    {
        width = boardConfig.width;
        height = boardConfig.height;

        logicalBoard = new int[width, height];
        graphicalBoard = new SpriteRenderer[width, height];

        tetrominoManager.CreatePool();

        InitializeBoard();
        
    }

    public override void Tick()
    {
       AutoFall();
    }


    private void MoveIssued(int x, int y)
    {   
        //print((currentPosX + x , currentPosY + y));
        if(CanMove(x, y))
        {
            currentPosX += x;
            currentPosY += y;

            Move();
        }

    }


    private void RotateIssued(int dir)
    {
        if(CanRotate(dir))
        {
            Move();
        }
    }


    private void SnapIssued()
    {
        DisplayBoard();
        SnapToGhost();
    }
    
    
    /*private void GetInputs()
    {

        //Move Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMove(-1, 0))
            {
                currentPosX -= 1;

                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();
                Move();
            }

        }


        //Move Right
        if (Input.GetKeyDown(KeyCode.D))
        {

            if (CanMove(1, 0))
            {
                currentPosX += 1;

                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();
                Move();
            }

        }


        //Move Down
        if (Input.GetKeyDown(KeyCode.S))
        {
           

            if (CanMove(0, -1))
            {
                currentPosY -= 1;

                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();
                Move();
            }



        }



         //Rotate Piece AC
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (CanRotate(1))
            {

                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();
                Move();
            }


        }


        //Rotate Piece CC
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CanRotate(-1))
            {
                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();
                Move();

            }


        }

        if(Input.GetKeyDown(KeyCode.W))
        {   
            DisplayBoard();
            SnapToGhost();
        }


    }*/


   

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

    
        GetTetromino();
 
    }


    //Disables the tetromino sprite or ghost sprite 
    //And sends it back to the pool
    private void DisableTetrominoSprite(SpriteRenderer sr)
    {
        tetrominoManager.DisableTetrominoSprite(sr);
    }

    //Disables the active tetromino
    private void DisableTetromino()
    {
       tetrominoManager.DisableTetromino(T);

    }

    //Gets the tetromino from pool
    private void GetTetromino()
    {
         //Middle column of the board
        currentPosX = width / 2 - 1;

        //Topmost row of the board
        currentPosY = height - 3;

        T = tetrominoManager.GetTetromino();
        currPiece = T.GetTetrominoMatrix(); 

        DisplayPiece(currentPosX, currentPosY);
        Ghost();
        removing = false;

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
                /*if (logicalBoard[x, y] != 100 && logicalBoard[x,y] != 9)
                {                                  
                    //graphicalBoard[x, y] = tetrominoManager.GetTetrominoSprite(logicalBoard[x,y]);
                    /*if(graphicalBoard[x, y] != null && !graphicalBoard[x,y].gameObject.activeInHierarchy)
                    {
                        graphicalBoard[x,y].gameObject.SetActive(true);
                    } 
                    
                }

                //Boundary
                else if (logicalBoard[x, y] == 9)
                {
                    //graphicalBoard[x, y].color = boundaryColor;
                }*/

                //Empty Space and to disable previous tetromino sprites before tetromino is locked
                if (logicalBoard[x, y] == 100)
                {   
                
                    //If there is a tetromino sprite or ghost sprite and it is not locked. Remove it
                    if(graphicalBoard[x, y] != null && graphicalBoard[x,y].gameObject.activeInHierarchy)
                    {   
                        
                        SpriteRenderer sr = graphicalBoard[x, y];
                        //graphicalBoard[x, y].gameObject.SetActive(false);
                        //graphicalBoard[x, y].transform.parent = this.transform;
                        graphicalBoard[x,y] = null;
                        DisableTetrominoSprite(sr);

                     
                        
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
                                
                                T.SetSprite(currPieceSprite);

                                graphicalBoard[boardX, boardY] = currPieceSprite;

                               
                    
                                                          
                                
                            }


                          
                        }
                    }
                                                          
            }
        }
    }


   
    
    private void Move()
    {
        DisplayBoard();
        DisplayPiece(currentPosX, currentPosY);
        Ghost();
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


    /// <summary>
    /// Snap the tetromino to its ghost
    /// </summary>
    private void SnapToGhost()
    {   

        currentPosX = ghostPosX;
        currentPosY = ghostPosY;
        DisplayPiece(ghostPosX, ghostPosY);
  
        //TODO: Option to lock immidiately or give 1 extra move
        LockPiece();
        
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
    
    bool removing = false;
    //Make pieces fall automatically
    private void AutoFall()
    {
        /*if(piecesLocked % 10 == 0)
        {
           // fallGap -= 0.2f;
        }*/

        if(removing)
        return;

        //Wait for desired time
        if (timer <= 0.5f)
        {
            //TUT: EXPLAIN TIME.DELTA TIME IMPORTANCE
            timer += Time.deltaTime;
        }

        else
        {
            //check if piece can fit one step down
            //PIECES NEED TO AUTOFALL ONLY IF THEY ARE NOT LOCKED
            //MAINLY FOR DEBUGGING PURPOSES
            if (CanMove(0, -1) )//&& !isLocked)
            {
                currentPosY -= 1;
                Move();
                //DisplayBoard();
                //DisplayPiece(currentPosX, currentPosY);
                //Ghost();             
            }

            //If it cannot stop and lock the piece on board
            else
            {           
                LockPiece(); 
                         
            }

            //MoveIssued(0, - 1);
            timer = 0;
        }

    }


    private void LockPiece()
    {
        removing = true;
        //TUT : y -> Y-axis, x -> X-axis
        for (int x = 0; x < currPiece.GetLength(1); x++)
        {
            for (int y = 0; y < currPiece.GetLength(0); y++)
            {
                //Piece position on board
                //TUT: EXPLAIN THIS
                int boardX = x + currentPosX;
                int boardY = y + currentPosY;

                //Check if inside boundaries, otherwise don't do anything
                //TUT: EXPLAIN WHAT HAPPENS IF OUTSIDE BOUNDARY
                //wE SIMPLY DO NOT CARE
                if (boardX >= 0 && boardX < width)
                {
                    if (boardY >= 0 && boardY < height)
                    {
                        //If its a tetromino position from tetromino matrix
                        if (currPiece[y, x] == 1)
                        {
                            //Lock the piece logically                          
                           //isLocked = true;
                          
                           logicalBoard[boardX, boardY] = T.GetTetrominoID(); 

                            //After locking the tetromino, its sprites becomes part of the board
                           if(graphicalBoard[boardX, boardY] != null)  
                                graphicalBoard[boardX, boardY].transform.parent = this.transform;         

                        }
                    }
                }

            }
        }


        CheckLineCompletion();
        DisableTetromino();
        GetTetromino(); 
    }


    private void DecreaseLine(int y) 
    {
        for (int x = 1; x < width - 1; x++) 
        {
            
            // Move one towards bottom
            logicalBoard[x, y-1] = logicalBoard[x, y];
            graphicalBoard[x, y-1] = graphicalBoard[x , y];

            graphicalBoard[x, y] = null;

            // Update Block position
            if(graphicalBoard[x , y - 1] != null)
            {
                graphicalBoard[x, y-1].transform.position += new Vector3(0, - (1 + boardConfig.offY), 0);
            }
            
            
            
        }
    }



    private void DecreaseLinesAboveOf(int y) 
    {
        for (int i = y; i < height; ++i)
            DecreaseLine(i);
    }


    private void RemoveLineAt(int y) 
    {
        for (int x = 1; x < width - 1; x++) 
        {
            logicalBoard[x, y] = 100;
            graphicalBoard[x,y].gameObject.SetActive(false);
            graphicalBoard[x, y] = null;
        }

        //DisplayBoard();
    }

    private bool IsLineCompleteAt(int y)
    {
        for (int x = 1; x < width - 1; x++)
        {
            if (logicalBoard[x, y] == 100)
            {
                 return false;
            }
               
        }
            
        return true;
    }

    private void CheckLineCompletion() 
    {
        
        for (int y = 1; y < height; y++) 
        {
            if (IsLineCompleteAt(y)) 
            {   
                
                RemoveLineAt(y);
                DecreaseLinesAboveOf(y+1);
                y--;
            }
        }
        
        
        
    }

    
}
