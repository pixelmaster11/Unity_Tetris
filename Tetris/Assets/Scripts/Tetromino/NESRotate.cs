using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

/// <summary>
/// This class performs NES rotation method
/// </summary>
public class NESRotate : IRotateStrategy
{
   

    // I rotations
    private readonly int[,] I_1 =  {{ 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 } };

    private readonly int[,] I_2 =  {{ 0 , 0 , 0 , 0 },
                                    { 0 , 0 , 0 , 0 },
                                    { 1 , 1 , 1 , 1 },
                                    { 0 , 0 , 0 , 0 } };

    private readonly int[,] I_3 =  {{ 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 } };

    private readonly int[,] I_4 =  {{ 0 , 0 , 0 , 0 },
                                    { 0 , 0 , 0 , 0 },
                                    { 1 , 1 , 1 , 1 },
                                    { 0 , 0 , 0 , 0 } };                                    

                    

    //L Rotations
    private readonly int[,] L_1 =  {{ 0 , 0 , 0 , 0 },
                                    { 0 , 1 , 1 , 0 },
                                    { 0 , 1 , 0 , 0 },
                                    { 0 , 1 , 0 , 0 } };

    private readonly int[,] L_2 =  {{ 0 , 0 , 0 , 0 },
                                    { 0 , 0 , 0 , 0 },
                                    { 1 , 1 , 1 , 0 },
                                    { 0 , 0 , 1 , 0 } };

    private readonly int[,] L_3 =  {{ 0 , 0 , 0 , 0 },
                                    { 0 , 1 , 0 , 0 },
                                    { 0 , 1 , 0 , 0 },
                                    { 1 , 1 , 0 , 0 } };

    private readonly int[,] L_4 =  {{ 0 , 0 , 0 , 0 },
                                    { 1 , 0 , 0 , 0 },
                                    { 1 , 1 , 1 , 0 },
                                    { 0 , 0 , 0 , 0 } };


    // J Rotations
    private readonly int[,] J_1 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 } };

    private readonly int[,] J_2 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 0 , 1 },
                                     { 0 , 1 , 1 , 1 },
                                     { 0 , 0 , 0 , 0 } };

    private readonly int[,] J_3 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 1 , 1 } };

    private readonly int[,] J_4 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 0 , 0 },
                                     { 0 , 1 , 1 , 1 },
                                     { 0 , 1 , 0 , 0 } };                                                                                                     


    //T Rotations
    private readonly int[,] T_1 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 } };

    private readonly int[,] T_2 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 1 , 1 , 1 },
                                     { 0 , 0 , 0 , 0 } };

    private readonly int[,] T_3 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 1 , 1 },
                                     { 0 , 0 , 1 , 0 } };

    private readonly int[,] T_4 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 0 , 0 },
                                     { 0 , 1 , 1 , 1 },
                                     { 0 , 0 , 1 , 0 } };                                                                                                              


    //S Rotations
    private readonly int[,] S_1 =   {{ 0 , 0 , 1 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 1 , 0 , 0 },
                                     { 0 , 0 , 0 , 0 } };
    
    private readonly int[,] S_2 =   {{ 0 , 0 , 0 , 0 },
                                     { 1 , 1 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };


    private readonly int[,] S_3 =   {{ 0 , 0 , 1 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 1 , 0 , 0 },
                                     { 0 , 0 , 0 , 0 } };
    
    private readonly int[,] S_4 =   {{ 0 , 0 , 0 , 0 },
                                     { 1 , 1 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };                                     

    //Z Rotations
    private readonly int[,] Z_1 =   {{ 0 , 1 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };

    private readonly int[,] Z_2 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 1 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };    

    private readonly int[,] Z_3 =   {{ 0 , 1 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };

    private readonly int[,] Z_4 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 0 , 1 , 1 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };

    private readonly int[,] O_1 =   {{ 0 , 0 , 0 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 1 , 1 , 0 },
                                     { 0 , 0 , 0 , 0 } };                                                                                                                
    
    //Lists to store individual piece rotations
    private List<int[,]> I_pieces;
    private List<int[,]> T_pieces;
    private List<int[,]> O_pieces;
    private List<int[,]> S_pieces;
    private List<int[,]> Z_pieces;
    private List<int[,]> J_pieces;
    private List<int[,]> L_pieces;

    //Dictionary to store all piece lists
    private Dictionary<TetrominoType, List<int[,]> > ALL_pieces;

    public NESRotate()
    {   
        //Initialize lists and add rotations
        I_pieces = new List<int[,]>();
        T_pieces = new List<int[,]>();
        O_pieces = new List<int[,]>();
        S_pieces = new List<int[,]>();
        Z_pieces = new List<int[,]>();
        J_pieces = new List<int[,]>();
        L_pieces = new List<int[,]>();

        //1st matrix should be the same as in the corresponding tetromino config file for convenience
        I_pieces.Add(I_1);
        I_pieces.Add(I_2);
        I_pieces.Add(I_1);
        I_pieces.Add(I_2);

        
        T_pieces.Add(T_1);
        T_pieces.Add(T_2);
        T_pieces.Add(T_3);
        T_pieces.Add(T_4);

        O_pieces.Add(O_1);
        O_pieces.Add(O_1);
        O_pieces.Add(O_1);
        O_pieces.Add(O_1);

        S_pieces.Add(S_1);
        S_pieces.Add(S_2);
        S_pieces.Add(S_1);
        S_pieces.Add(S_2);
     
        Z_pieces.Add(Z_1);
        Z_pieces.Add(Z_2);
        Z_pieces.Add(Z_1);
        Z_pieces.Add(Z_2);

        J_pieces.Add(J_1);
        J_pieces.Add(J_2);
        J_pieces.Add(J_3);
        J_pieces.Add(J_4);

        L_pieces.Add(L_1);
        L_pieces.Add(L_2);
        L_pieces.Add(L_3);
        L_pieces.Add(L_4);

      
        //Add all piece rotation lists to dictionary
        ALL_pieces = new Dictionary<TetrominoType, List<int[,]>>();
        ALL_pieces.Add(TetrominoType.I, I_pieces);
        ALL_pieces.Add(TetrominoType.T, T_pieces);
        ALL_pieces.Add(TetrominoType.O, O_pieces);
        ALL_pieces.Add(TetrominoType.S, S_pieces);
        ALL_pieces.Add(TetrominoType.Z, Z_pieces);
        ALL_pieces.Add(TetrominoType.J, J_pieces);
        ALL_pieces.Add(TetrominoType.L, L_pieces);
      

    }

    /// <summary>
    /// Perform NES rotaiton
    /// </summary>
    /// <param name="piece">Current tetromino matrix</param>
    /// <param name="rotateDir">CC / AC rotation </param>
    /// <param name="T">Referene to current tetromino</param>
    /// <returns></returns>
    public int[,] Rotate(int[,] piece, int rotateDir, Tetromino T)
    {   

        int pieceID = T.GetTetrominoID();
        int rotID = T.RotateID;

        //Set proper rotation and clamp within list limits
        rotID += rotateDir;

        if(rotID < 0)
            rotID = 3;
        else if(rotID > 3)
            rotID = 0;


        List<int [,]> pieceRotations;
        
        //Get rotation matrix based on proper id and type
        if(ALL_pieces.TryGetValue(T.GetTetrominoType(), out pieceRotations))
        {
            int[,] rotatedMatrix = pieceRotations[rotID];
            return rotatedMatrix; 
        }
      
        else
        {
            DebugUtils.LogError(this, T.GetTetrominoType() + " is not a valid tetromino type");
            return piece;
        }
        
        
       
    }

    
}
