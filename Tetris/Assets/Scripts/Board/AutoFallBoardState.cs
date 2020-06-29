using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

public class AutoFallBoardState : BoardState
{

    
    private float timer;
    private TetrominoManager tetrominoManager;
  
    private int ghostPosX, ghostPosY;


    public AutoFallBoardState(Board _board, BoardStateController _controller, TetrominoManager _tMan) : base(_board, _controller)
    {
        tetrominoManager = _tMan;
    }

    public override void Entry()
    {   
       

        EventManager.MoveEvent += MoveIssued;
        EventManager.RotateEvent += RotateIssued;
        EventManager.SnapEvent += SnapIssued;

        GetTetromino();
    }

    public override void Exit()
    {   
      

        EventManager.MoveEvent -= MoveIssued;
        EventManager.RotateEvent -= RotateIssued;
        EventManager.SnapEvent -= SnapIssued;
    }

    public override void StateUpdate()
    {
        AutoFall();
        
    }


    public void MoveIssued(int x, int y)
    {   
        //print((currentPosX + x , currentPosY + y));
        if(CanMove(x, y))
        {
            board.currentPosX += x;
            board.currentPosY += y;

            Move();
        }

    }


    public void RotateIssued(int dir)
    {
        if(CanRotate(dir))
        {
            Move();
        }
    }


    public void SnapIssued()
    {
        DisplayBoard();
        SnapToGhost();
    }

    
    public void Move()
    {
        DisplayBoard();
        DisplayPiece(board.currentPosX, board.currentPosY);
        Ghost();
    }
    
    

    //Gets the tetromino from pool
    public void GetTetromino()
    {
        
        //Disable any previously active tetrominoes
        DisableTetromino();

        //Get new tetromino from pool
        board.tetromino = tetrominoManager.GetTetromino();
        board.currPiece = board.tetromino.GetTetrominoMatrix(); 

        //Middle column of the board
        board.currentPosX = boardConfig.width / 2 - 1;

        //Topmost row of the board
        board.currentPosY = boardConfig.height - 3;

       

        //Display the piece and its ghost
        DisplayPiece( board.currentPosX,  board.currentPosY);
        Ghost();

      

    }


     /// <summary>
    /// This function displays only the locked Tetromino Sprites and removes any not locked Tetromino Sprites
    /// </summary>
    public void DisplayBoard()
    {
        for (int y = 0; y < boardConfig.height; y++)
        {
            for (int x = 0; x < boardConfig.width; x++)
            {
               
                //Empty Space and to disable previous tetromino sprites before tetromino is locked
                if (board.logicalBoard[x, y] == BoardConfig.EMPTY_SPACE)
                {   
                
                    //If there is a tetromino sprite or ghost sprite and it is not locked. Remove it
                    if(board.graphicalBoard[x, y] != null && board.graphicalBoard[x,y].gameObject.activeInHierarchy)
                    {   
                        
                        SpriteRenderer sr = board.graphicalBoard[x, y];
                        //graphicalBoard[x, y].gameObject.SetActive(false);
                        //graphicalBoard[x, y].transform.parent = this.transform;
                        board.graphicalBoard[x,y] = null;
                        DisableTetrominoSprite(sr);

                                            
                    }                  
                    
                }

            }
        }
    }


     //Displays or locks pieces on board
    public void DisplayPiece(int currPosX, int currPosY, bool isGhost = false)
    {
        
    
      //GEtLength(0) = Rows / Height
        for (int x = 0; x <  board.currPiece.GetLength(1); x++)
        {
            for (int y = 0; y <  board.currPiece.GetLength(0); y++)
            {       
                    
                    //Piece position on board
                    //TUT: EXPLAIN THIS
                    int boardX = x + currPosX;//currentPosX; 
                    int boardY = y + currPosY; //currentPosY; 

                //Check if inside boundaries, otherwise don't do anything
                //TUT: EXPLAIN WHAT HAPPENS IF OUTSIDE BOUNDARY
                //wE SIMPLY DO NOT CARE
                    if (boardX > 0 && boardX < (boardConfig.width - 1))
                    {
                        if (boardY > 0 && boardY < boardConfig.height)
                        {
                            //If its a tetromino position from tetromino matrix
                            //TUT: IMPORTANCE OF ISLOCKED
                            //SHOW USING COROUTINES THAT THE LOCKED PIECES DEOS NOT GET WIPED OUT AS IT IS DISPLAYED  CONSTANTLY USING THIS FUNCTION
                            //WE ONLY NEED TO DISPLAY THE PIECE GRAPHICALLY TILL ITS LOCKED LOGICALLY
                            //ONCE ITS LOGICALLY LOCKED THE BOARD WILL DISPLAY THE PIECE
                            if (board.currPiece[y, x] == 1 && board.graphicalBoard[boardX, boardY] == null)  //&& !isLocked)
                            {    
                                                     
                                //Display only piece graphic                       
                                //graphicalBoard[pieceOnBoardX, pieceOnBoardY].color = tetrominos[randomPieceID].tetrominoColor;

                                SpriteRenderer currPieceSprite;

                               
                                //Get the sprite associated with current Tetromino piece
                                currPieceSprite = tetrominoManager.GetTetrominoSprite(board.tetromino.GetTetrominoID(), isGhost);

                                
                                currPieceSprite.gameObject.transform.position = new Vector2(boardX + boardX * boardConfig.offX,
                                                                                            boardY + boardY * boardConfig.offY);

                                currPieceSprite.gameObject.SetActive(true);

                                currPieceSprite.transform.parent =  board.tetromino.transform;
                                
                                board.tetromino.SetSprite(currPieceSprite);

                                board.graphicalBoard[boardX, boardY] = currPieceSprite;

                               
                    
                                                          
                                
                            }


                          
                        }
                    }
                                                          
            }
        }
    }


    //DisplayGhost Piece
    public void Ghost()
    {      
        int rd = 1;
        while(CanMove(0, -rd))
        {        
            rd ++;        
        }

        if(rd > 1)
        {
            ghostPosX =  board.currentPosX;
            ghostPosY =  board.currentPosY + 1 - rd;
            DisplayPiece(ghostPosX, ghostPosY, true);
        }
       
              
    }


     //Can we fit our piece after doing the next move 
    public bool CanMove(int xDir, int yDir)
    {
        //next piece positions to be performed
        int nextPosX =  board.currentPosX + xDir;
        int nextPosY =  board.currentPosY + yDir;

        for (int x = 0; x <  board.currPiece.GetLength(1); x++)
        {
            for (int y = 0; y <  board.currPiece.GetLength(0); y++)
            {
                //Move next piece positions on board
                int pieceOnBoardX = x + nextPosX;
                int pieceOnBoardY = y + nextPosY;           

                //If next positions are inside the boundary
                if (pieceOnBoardX >= 0 && pieceOnBoardX < boardConfig.width)
                {
                    if (pieceOnBoardY >= 0 && pieceOnBoardY < boardConfig.height)
                    {
                        //Cannot fit piece
                        //TUT: EXPLAIN WHY CANNOT FIT?
                        //WE ARE CHECKING PIECE MATRIX POSITIONS AND PIECE ON BOARD POSITIONS
                        if (board.currPiece[y, x] != 0 && board.logicalBoard[pieceOnBoardX, pieceOnBoardY] != BoardConfig.EMPTY_SPACE)
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
    public void SnapToGhost()
    {   

        board.currentPosX = ghostPosX;
        board.currentPosY = ghostPosY;
        DisplayPiece(ghostPosX, ghostPosY);
  
        //TODO: Option to lock immidiately or give 1 extra move
        //LockPiece();

        stateController.ChangeState(BoardStateType.LockingState);
        
    }


    
   //Can we fit our piece after rotating?
    public bool CanRotate(int rotDirection)
    {   
        

        //Rotate the piece
        int [,] rotatedPiece = Rotate(board.currPiece, rotDirection);

        //Check if rotated piece can fit on board
        for (int x = 0; x < rotatedPiece.GetLength(0); x++)
        {
            for (int y = 0; y < rotatedPiece.GetLength(1); y++)
            {
                int pieceOnBoardX = x +  board.currentPosX;
                int pieceOnBoardY = y +  board.currentPosY;

                //If inside boundary
                if (pieceOnBoardX >= 0 && pieceOnBoardX < boardConfig.width)
                {
                    if (pieceOnBoardY >= 0 && pieceOnBoardY < boardConfig.height)
                    {
                        //Cannot fit
                        //If it is a sprite in the tetromino matrix and the board is not empty (Either other sprites or boundary is present)
                        if (rotatedPiece[y, x] == 1 && board.logicalBoard[pieceOnBoardX, pieceOnBoardY] != BoardConfig.EMPTY_SPACE)
                        {
                            
                            return false;
                        }
                    }
                }

            }
        }

        //Piece can fit succesfully
        //Assign current piece as rotated piece
        board.currPiece = rotatedPiece;
        return true;
    }


    //Rotate by 90 deg in CC and AC
    public int[,] Rotate (int[,] piece, int direction)
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
    

    //Auto Fall / Input State
    bool removing = false;

    //Make pieces fall automatically
    public void AutoFall()
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
                board.currentPosY -= 1;
                //Move();
                DisplayBoard();
                DisplayPiece(board.currentPosX, board.currentPosY);
                Ghost();             
            }

            //If it cannot stop and lock the piece on board
            else
            {           
                //LockPiece(); 
                stateController.ChangeState(BoardStateType.LockingState);
            }

          
            timer = 0;
        }

    }



     //Disables the tetromino sprite or ghost sprite 
    //And sends it back to the pool
    public void DisableTetrominoSprite(SpriteRenderer sr)
    {
        tetrominoManager.DisableTetrominoSprite(sr);
    }

    //Disables the active tetromino
    public void DisableTetromino()
    {
       tetrominoManager.DisableTetromino(board.tetromino);

    }

}
