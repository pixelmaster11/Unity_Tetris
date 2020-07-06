using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using System.Linq;

/// <summary>
/// Class responsible for managing gameplay UI
/// </summary>
public class GameUIManager : MonoBehaviour
{   

    [Header("Preview Tetrominos")]
    //UI positions to display next pieces
    [SerializeField]
    private Transform [] previewPositions;

    //Transform to clean up scene
    [SerializeField]
    private Transform previewTransform;

    //Queue to keep track of next pieces
    private Queue<PreviewTetromino> previewTetrominos;

    //Factory to get preview pieces
    private PreviewTetrominoFactory previewTetrominoFactory;

    //Transform to store all pooled preview pieces
    [SerializeField]
    private Transform poolTransform;

    [Header("Holded Tetromino")]
    //UI position to display hold piece
    [SerializeField]
    private Transform holdPosition;

    //Hold piece parent 
    [SerializeField]
    private Transform holdTransform;

    //Currently holded piece preview
    private PreviewTetromino holdTetromino;

    //How many preview pieces to display
    private int maxQueue;

    //Subscribe to Events
    private void OnEnable()
    {
        EventManager.TetrominoSpawnEvent += DisplayPreview;
        EventManager.HoldPieceEvent += DisplayHoldPiece;
    }

    //Unsubscribe to Events
    private void OnDisable()
    {
        EventManager.TetrominoSpawnEvent -= DisplayPreview;
        EventManager.HoldPieceEvent -= DisplayHoldPiece;
    }

    //Sets the preview factory
    public void SetSpawnConfig(BaseConfig config)
    {   
        TetrominoSpawnConfig spawnConfig = (TetrominoSpawnConfig) config;
        previewTetrominoFactory = new PreviewTetrominoFactory(spawnConfig.PreviewTetrominoPrefabs, 
                                  spawnConfig.MaxTetrominosInQueue, poolTransform);

        previewTetrominoFactory.CreatePool();

        maxQueue = spawnConfig.MaxTetrominosInQueue;
        previewTetrominos = new Queue<PreviewTetromino>();
    }

    /// <summary>
    /// Display the next preview pieces in queue
    /// </summary>
    /// <param name="tetromino"></param>
    private void DisplayPreview(Tetromino tetromino)
    {   
        //Get id and rot of current piece
        int id = tetromino.GetTetrominoID();
        int rotID = tetromino.RotateID;

        //Get the preview piece of matching id
        PreviewTetromino p = previewTetrominoFactory.GetPreview(id);

        //Set rotation of preivew piece
        p.RotID = rotID;

        //Invert rotations for T, L, J shapes
        if(id == 1 || id == 5 || id == 6)
        {
            p.transform.eulerAngles = new Vector3(0 , 0 , -90 * rotID);
        }

        else
        {
            p.transform.eulerAngles = new Vector3(0 , 0 , 90 * rotID);
        }

        previewTetrominos.Enqueue(p);
        p.transform.parent = previewTransform;
        p.gameObject.SetActive(true);
       
        //If total previews exceed maximum 
        if(previewTetrominos.Count > maxQueue)
        {
            //Disable the first preview from queue to only display maximum allowed previews
            PreviewTetromino pt = previewTetrominos.Dequeue();
            pt.gameObject.SetActive(false);
            pt.transform.Rotate(new Vector3(0 , 0 , 0));
            pt.transform.parent = poolTransform;
            
            //Set proper positions as we need to shift all up by 1 
            SetPositions();
        }
            
    }


    /// <summary>
    /// Sets the positions of preview pieces
    /// </summary>
    private void SetPositions()
    {
        for(int i = 0; i < previewTetrominos.Count; i++)
        {
            previewTetrominos.ElementAt(i).transform.position = previewPositions[i].position;
           
        }
    }

   
   /// <summary>
   /// Displays the currently holded piece preview
   /// </summary>
   /// <param name="id">id of tetromino</param>
   /// <param name="rotID">Current rotation of tetromino</param>
    private void DisplayHoldPiece(int id, int rotID)
    {   
        //If there is any previously holded piece
        if(holdTetromino != null)
        {
            //Disable it and replace it with new
            holdTetromino.gameObject.SetActive(false);
            holdTetromino.transform.Rotate(new Vector3(0 , 0 , 0));
            holdTetromino.transform.parent = poolTransform;
        }
        
        //Get matching hold piece preview and display it
        holdTetromino = previewTetrominoFactory.GetPreview(id);
        holdTetromino.transform.position = holdPosition.position;
        holdTetromino.transform.parent = holdTransform;
        holdTetromino.gameObject.SetActive(true);
        holdTetromino.RotID = rotID;

        //Invert rotations for T, L, J shapes
        if(id == 1 || id == 5 || id == 6)
        {
            holdTetromino.transform.eulerAngles = new Vector3(0 , 0 , -90 * rotID);
        }

        else
        {
            holdTetromino.transform.eulerAngles = new Vector3(0 , 0 , 90 * rotID);
        }

        
    }


    

}
