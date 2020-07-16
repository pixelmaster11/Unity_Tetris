using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using System.Linq;
using UnityEngine.UI;

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

    [SerializeField]
    private AnimationCurve posXOffsetCurve;

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


    [Header("Texts")]

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text linesText;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text timeText;

    private float timer; 

    [Header("Gameover Canvas")]
    [SerializeField]
    private Canvas gameOverCanvas;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;


    //Subscribe to Events
    private void OnEnable()
    {
        EventManager.TetrominoSpawnEvent += DisplayPreview;
        EventManager.HoldPieceEvent += DisplayHoldPiece;
        EventManager.LineCompleteEvent += DisplayLinesCleared;
        EventManager.GameOverEvent += DisplayGameOverUI;
        
        playButton.onClick.AddListener(OnRestart);
        quitButton.onClick.AddListener(OnQuit);
    }

    //Unsubscribe to Events
    private void OnDisable()
    {
        EventManager.TetrominoSpawnEvent -= DisplayPreview;
        EventManager.HoldPieceEvent -= DisplayHoldPiece;

        EventManager.LineCompleteEvent -= DisplayLinesCleared;
        EventManager.GameOverEvent -= DisplayGameOverUI;

        playButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }


    public void UIUpdate()
    {
        timer += Time.deltaTime;
        DisplayTime();
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
            PreviewTetromino pt = previewTetrominos.ElementAt(i);
    
            pt.transform.position = previewPositions[i].position;
            pt.transform.position += new Vector3(posXOffsetCurve.Evaluate(pt.RotID ), 0, 0);

            
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
        holdTetromino.RotID = rotID;
        holdTetromino.transform.position = holdPosition.position;
        holdTetromino.transform.position += new Vector3(posXOffsetCurve.Evaluate(holdTetromino.RotID),0, 0);
        holdTetromino.transform.parent = holdTransform;
        holdTetromino.gameObject.SetActive(true);

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



    private void DisplayScore()
    {

    }


    private void DisplayLinesCleared(int linesCleared)
    {
        linesText.text = linesCleared.ToString();
    }


    private void DisplayTime()
    {
        timeText.text = ((int)timer).ToString();
    }


    private void DisplayLevel()
    {

    }
    

    private void DisplayGameOverUI()
    {
        gameOverCanvas.gameObject.SetActive(true);
        DisablePreviewTetrominos();
        DisableHoldTetromino();
    }

    private void OnRestart()
    {
        gameOverCanvas.gameObject.SetActive(false);

        if(EventManager.GameOverRestartEvent != null)
        {
            EventManager.GameOverRestartEvent();
        }
    }

    private void OnQuit()
    {
        gameOverCanvas.gameObject.SetActive(false);

        if(EventManager.GameOverQuitEvent != null)
        {
            EventManager.GameOverQuitEvent();
        }
    }


    private void DisablePreviewTetrominos()
    {
        foreach(PreviewTetromino pt in previewTetrominos)
        {
            pt.gameObject.SetActive(false);
            pt.transform.parent = poolTransform;

        }

        previewTetrominos.Clear();
    }


    private void DisableHoldTetromino()
    {
        if(holdTetromino != null)
        {
            holdTetromino.gameObject.SetActive(false);
            holdTetromino = null;
            holdTetromino.transform.parent = poolTransform;
        }
      
    }

 
}
