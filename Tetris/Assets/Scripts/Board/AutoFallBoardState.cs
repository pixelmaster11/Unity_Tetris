using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// Main board state where most of things happen.
    /// Responsible for executing input commands, tetromino fall, display piece, update board, etc.
    /// </summary>
    public class AutoFallBoardState : BoardState
    {
        //Timer for autofalling
        private float timer;

        //Time delay between piece autofall
        private float fallTime;
        private float fallMultiplier;

        //Total locked pieces
        private float lockedPieces; 


        //Reference to tetromino manager to enable/  disable tetorminoes and sprites
        private TetrominoManager tetrominoManager; 

        //Ghost piece positions
        private int ghostPosX, ghostPosY;

        //Rotation strategy
        private IRotateStrategy rotateStrategy;

        //On Create
        public AutoFallBoardState(Board _board, BoardStateController _controller, TetrominoManager _tMan) : base(_board, _controller)
        {
            //Cache tetromino manager reference
            tetrominoManager = _tMan;

            //Set fall timers
            fallTime = boardConfig.fallTime;
            fallMultiplier = boardConfig.fallMultiplier;
            lockedPieces = 0;

            //Set proper rotation method
            SetRotation();
        }

        //Subscribe to Input Command Events
        public override void Entry()
        {        
            EventManager.MoveEvent += MoveIssued;
            EventManager.RotateEvent += RotateIssued;
            EventManager.SnapEvent += SnapIssued;

            //Get a new tetromino
            GetTetromino();
        }

        //Unsubscribe from Input Command Events as we only accept inputs in this state only
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

        /// <summary>
        /// Set proper Rotation algorithm based on the chosen configuration
        /// </summary>
        private void SetRotation()
        {
            switch(boardConfig.rotationType)
            {
                case RotateType.Matrix:
                rotateStrategy = new MatrixRotate();
                break;

                case RotateType.Nintendo:
                rotateStrategy = new NESRotate();
                break;
                
                default:
                rotateStrategy = new MatrixRotate();
                break;

            }
        }

        /// <summary>
        /// Subscribed for Move Command
        /// </summary>
        /// <param name="nextX"></param>
        /// <param name="nextY"></param>
        private void MoveIssued(int nextX, int nextY)
        {   
            //Check if piece can move in the next desired pos
            if(CanMove(nextX, nextY))
            {   
                //Update pos and board if it can move
                board.currentPosX += nextX;
                board.currentPosY += nextY;

                UpdateBoard();
            }

        }

        /// <summary>
        /// Subscribed to Rotate Command
        /// </summary>
        /// <param name="dir">Rotation dir cc / ac</param>
        private void RotateIssued(int dir)
        {   
            //If Can rotate then update board
            if(CanRotate(dir))
            {
                UpdateBoard();
            }
        }

        /// <summary>
        /// Subscribed to Snap Command
        /// </summary>
        private void SnapIssued()
        {   
            //Disable previous ghost sprites then snap piece on the ghost
            DisplayBoard();
            SnapToGhost();
        }

        /// <summary>
        /// Update the board if piece is moved or rotated
        /// </summary>
        private void UpdateBoard()
        {
            //Disables any previous picece sprites
            DisplayBoard();

            //Display sprites at new positions
            DisplayPiece(board.currentPosX, board.currentPosY);

            //Display new ghost 
            Ghost();
        }
        
        
        //Gets the tetromino from pool
        private void GetTetromino()
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
        private void DisplayBoard()
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
        private void DisplayPiece(int currPosX, int currPosY, bool isGhost = false)
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
        private void Ghost()
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
        private bool CanMove(int xDir, int yDir)
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
        private void SnapToGhost()
        {   

            board.currentPosX = ghostPosX;
            board.currentPosY = ghostPosY;
            DisplayPiece(ghostPosX, ghostPosY);
    
            //TODO: Option to lock immidiately or give 1 extra move
            LockPiece();

            //stateController.ChangeState(BoardStateType.LockingState);
            
        }

        
    //Can we fit our piece after rotating?
        private bool CanRotate(int rotDirection)
        {   
            

            //Rotate the piece using the rotation strategy
            int [,] rotatedPiece = rotateStrategy.Rotate(board.currPiece, rotDirection, board.tetromino);

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

            //Set the rotation ID if successfull rotation
            board.tetromino.SetTetrominoRotation(rotDirection);

            //Assign current piece as rotated piece
            board.currPiece = rotatedPiece;

            return true;
        }


        //Make pieces fall automatically
        private void AutoFall()
        {

            //Wait for desired time
            if (timer <= fallTime)
            {
                //TUT: EXPLAIN TIME.DELTA TIME IMPORTANCE
                timer += Time.deltaTime;
            }

            else
            {
                //check if piece can fit one step down then make it auto fall down
                if (CanMove(0, -1) )
                {
                    board.currentPosY -= 1;
                    UpdateBoard();
           
                }

                //If it cannot stop and lock the piece on board
                else
                {                          
                    //stateController.ChangeState(BoardStateType.LockingState);
                    LockPiece();
                }

            
                timer = 0;
            }

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
        tetrominoManager.DisableTetromino(board.tetromino);

        }

        //Change to Lock state
        private void LockPiece()
        {   
            //Increment locked pieces
            lockedPieces ++;

            //Decrease fall delay till 0.1 seconds
            if(lockedPieces % 10 == 0 && fallTime >= 0.1f)
            {
                fallTime -= fallMultiplier;
            }

            //Change to Locking state
            stateController.ChangeState(BoardStateType.LockingState);
        }

    }

}

