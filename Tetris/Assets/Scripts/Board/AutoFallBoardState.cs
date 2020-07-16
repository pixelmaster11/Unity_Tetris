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
        private float fallPieces;

        //Reference to tetromino manager to enable/  disable tetorminoes and sprites
        private TetrominoManager tetrominoManager; 
        private BoardRenderer boardRenderer;

        //Ghost piece positions
        private int ghostPosX, ghostPosY;

        //Rotation strategy
        private IRotateStrategy rotateStrategy;
       

        private Tetromino holdPiece;
        private int[,] holdPieceMatrix;
        

        //On Create
        public AutoFallBoardState(Board _board, BoardStateController _controller, TetrominoManager _tMan) : base(_board, _controller)
        {
            //Cache tetromino manager reference
            tetrominoManager = _tMan;

            //Set Settings
            SetSettings();

            //Set proper rotation method
            SetRotation();

            boardRenderer = new BoardRenderer(_board, _tMan);
           
        }

#region State Methods

        //Subscribe to Input Command Events
        public override void Entry()
        {        
            
            EventManager.MoveEvent += MoveIssued;
            EventManager.RotateEvent += RotateIssued;
            EventManager.SnapEvent += SnapIssued;
            EventManager.HoldCommandEvent += HoldIssued;
            EventManager.GameOverEvent += OnGameOver;

            //Disable any previously active tetrominoes
            boardRenderer.DisableTetromino();

            //Get a new tetromino
            GetTetromino();

            if(IsGameOver())
            {
               stateController.ChangeState(BoardStateType.GameOverState);
            }

           
        }

        //Unsubscribe from Input Command Events as we only accept inputs in this state only
        public override void Exit()
        {   
            EventManager.MoveEvent -= MoveIssued;
            EventManager.RotateEvent -= RotateIssued;
            EventManager.SnapEvent -= SnapIssued;
            EventManager.HoldCommandEvent -= HoldIssued;
        }


        public override void StateUpdate()
        {
            AutoFall();      
        }

#endregion //State Methods

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

                    //Raise Move Success event
                    if(EventManager.MoveSuccessEvent != null)
                    {
                        EventManager.MoveSuccessEvent();
                    }
           
                }

                //If it cannot stop and lock the piece on board
                else
                {                          
                    LockPiece();
                }

            
                timer = 0;
            }

        }


        //Set proper Rotation algorithm based on the chosen configuration
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


        private void OnGameOver()
        {
            
            SetSettings();
            EventManager.GameOverEvent -= OnGameOver;
        }


        private void SetSettings()
        {
            //Set fall timers
            fallTime = boardConfig.fallTime;
            fallMultiplier = boardConfig.fallMultiplier;
            fallPieces = boardConfig.fallPieces;
            lockedPieces = 0;
        }

#region Input Commands

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

                //Raise Move Success event
                if(EventManager.MoveSuccessEvent != null)
                {
                    EventManager.MoveSuccessEvent();
                }
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

                //Raise Rotate Success event
                if(EventManager.RotateSuccessEvent != null)
                {
                    EventManager.RotateSuccessEvent();
                }
            }
        }

      
        //Subscribed to Snap Command
        private void SnapIssued()
        {   
            //Disable previous ghost sprites then snap piece on the ghost
            boardRenderer.DisplayBoard();
            SnapToGhost();

          
        }


        //Subscribed to Hold Command
        private void HoldIssued()
        {   

            //If no hold piece
            if(holdPiece == null)
            {
                //Assign current piece to hold and Get new one
                holdPiece = board.tetromino;
                holdPieceMatrix = board.currPiece;
                holdPiece.OnHold();

                GetTetromino();

                
            }

            //Current piece goes to hold and hold pieces becomes current
            else
            {                          
                GetTetromino(true);
            }
            
            //Raise Hold Piece Success event
            if(EventManager.HoldPieceEvent != null)
            {
                EventManager.HoldPieceEvent(holdPiece.GetTetrominoID(), holdPiece.RotateID);
            }        

            
            
        }

#endregion //Input Commands

#region Board Renders
        //Gets the tetromino from pool
        private void GetTetromino(bool isHoldedPiece = false)
        {
            //Swap Holded and Current Piece
            if(isHoldedPiece)
            {
                SwapHoldPiece();            
            }

            else
            {
                //Get new tetromino from pool and store it matrix
                board.tetromino = tetrominoManager.GetTetromino();
                board.currPiece = board.tetromino.GetTetrominoMatrix();

                //Sets initial rotation
                if(boardConfig.rotationType == RotateType.Matrix)
                {
                    for(int i = 0; i < board.tetromino.RotateID; i++)
                    {
                        board.currPiece = rotateStrategy.Rotate(board.currPiece, 1, board.tetromino);        
                    }
                }

                else
                {
                    board.currPiece = rotateStrategy.Rotate(board.currPiece, 0, board.tetromino); 
                }

            
                
            }
           
            //Middle column of the board
            board.currentPosX = boardConfig.width / 2 - 1;

            //Topmost row of the board
            board.currentPosY = boardConfig.height - 3;

            UpdateBoard();

       
        

        }

        // Update the board if piece is moved or rotated
        private void UpdateBoard()
        {
            //Disables any previous picece sprites
            boardRenderer.DisplayBoard();

            //Display sprites at new positions
            boardRenderer.DisplayPiece(board.currentPosX, board.currentPosY);

            //Display new ghost 
            Ghost();
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
                boardRenderer.DisplayPiece(ghostPosX, ghostPosY, true);
            }
        
                
        }

#endregion //Board Renders

#region Input Executions
        //Swaps the current and hold pieces
        private void SwapHoldPiece()
        {
            Tetromino temp = holdPiece;
            int[,] tempMatrix = holdPieceMatrix;

            //Change holded values on both pieces
            holdPiece.OnHold();
            board.tetromino.OnHold();

            holdPiece = board.tetromino;
            holdPieceMatrix = board.currPiece;
                
            board.tetromino = temp;
            board.currPiece = tempMatrix; 
            
            //If hold only allowed once, unsubscribe from hold input command
            if(boardConfig.holdType == HoldType.Once)
            {
                EventManager.HoldCommandEvent -= HoldIssued;
            }
                               
        }


        //Snap the tetromino to its ghost
        private void SnapToGhost()
        {   

            board.currentPosX = ghostPosX;
            board.currentPosY = ghostPosY;
            boardRenderer.DisplayPiece(ghostPosX, ghostPosY);


            if(EventManager.SnapParticlesEvent != null)
            {
                
                EventManager.SnapParticlesEvent(ghostPosX, ghostPosY);
            }

            //TODO: Option to lock immidiately or give 1 extra move
            LockPiece();
            
        }


        //Can we move our piece 
        private bool CanMove(int xDir, int yDir)
        {
            
            
            //next piece positions to be moved to
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


#endregion // Input Executions

       
        //Change to Lock state
        private void LockPiece()
        {   
            //Increment locked pieces
            lockedPieces ++;

            //Decrease fall delay till 0.1 seconds
            if(lockedPieces % fallPieces == 0 && fallTime >= 0.1f)
            {
                fallTime -= fallMultiplier;

                //Raise fall time decrease event         
                if(EventManager.FallTimeDecreaseEvent != null)
                {
                    EventManager.FallTimeDecreaseEvent(fallTime);
                }
            }

            //Change to Locking state
            stateController.ChangeState(BoardStateType.LockingState);
        }

        /// <summary>
        /// Function to check whether game is over at the start
        /// </summary>
        /// <returns>Returns true if starting piece itself cannot move down</returns>
        private bool IsGameOver()
        {
            if(!CanMove(0, -1))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }

}

