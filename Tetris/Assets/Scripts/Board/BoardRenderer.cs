using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

namespace BoardSystem
{


    public class BoardRenderer
    {
        
        private Board board;
        private BoardConfig boardConfig;
        private TetrominoManager tetrominoManager;


        public BoardRenderer(Board _board, TetrominoManager _tMan)
        {
            board = _board;
            tetrominoManager = _tMan;
            boardConfig = board.boardConfig;
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
                        int boardX = x + currPosX;
                        int boardY = y + currPosY; 

                    //Check if inside boundaries, otherwise don't do anything 
                    //IF OUTSIDE BOUNDARY wE SIMPLY DO NOT CARE
                        if (boardX > 0 && boardX < (boardConfig.width - 1))
                        {
                            if (boardY > 0 && boardY < boardConfig.height)
                            {
                                //If its a tetromino position from tetromino matrix
                                //WE ONLY NEED TO DISPLAY THE PIECE GRAPHICALLY TILL ITS LOCKED LOGICALLY
                                
                                if (board.currPiece[y,x] == 1 && board.graphicalBoard[boardX, boardY] == null) 
                                {    
                                                        
                                    

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


        //Disables the tetromino sprite or ghost sprite 
        //And sends it back to the pool
        private void DisableTetrominoSprite(SpriteRenderer sr)
        {
            tetrominoManager.DisableTetrominoSprite(sr);
        }


    }

  


}
